﻿using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace zaraga.weather.Services
{
    internal class ApiService
    {
        //Singleton instance
        private static ApiService? _instance;
        internal static ApiService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ApiService();
                }
                return _instance;
            }
        }

        private readonly bool _useRetries;
        private readonly int _maxRetries;
        private readonly zaraga.api.Client _apiClient;
        private int _retryCount;


        internal ApiService(bool useRetries = false, int maxRetries = 1)
        {
            _useRetries = useRetries;
            _maxRetries = maxRetries;
            _retryCount = 0;
            _apiClient = new zaraga.api.Client(BuildMetadata.BaseWeatherapiUrl);
        }

        internal async Task<T> GetRequest<T>(string url, T? handleResp) where T : new()
        {
            App.Log("Start call");
            App.Log($"GET : {url}");
            try
            {
                T? resp = await _apiClient.Get<T>(url, handleResp);
                App.Log($"Response received");
                App.Log(JsonConvert.SerializeObject(resp));
                if (resp != null)
                {
                    return resp;
                }
                else
                {
                    return handleResp != null ? handleResp : new T();
                }
            }
            catch (Exception ex)
            {
                App.Log(ex);
                if (_useRetries)
                {
                    if (_retryCount >= _maxRetries)
                    {
                        _retryCount = 0;
                        App.Log("Max retries reached");
                        throw;
                    }
                    else
                    {
                        _retryCount++;
                        App.Log($"Run retry {_retryCount} of {_maxRetries}");
                        return await GetRequest<T>(url, handleResp);
                    }
                }
                else
                {
                    throw;
                }
            }
        }

    }
}
