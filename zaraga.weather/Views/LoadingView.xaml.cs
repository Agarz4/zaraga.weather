using Android.Provider;
using Android.Speech;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace zaraga.weather.Views;

public partial class LoadingView : ContentView
{
    public LoadingView()
    {
        InitializeComponent();

        var lowerAnimation = new Animation(v => AnimatedProgressBar.LowerRangeValue = (float)v, -0.4, 1.0);
        var upperAnimation = new Animation(v => AnimatedProgressBar.UpperRangeValue = (float)v, 0.0, 1.4);

        lowerAnimation.Commit(this, "lower", length: 1000, easing: Easing.CubicInOut, repeat: () => true);
        upperAnimation.Commit(this, "upper", length: 1000, easing: Easing.CubicInOut, repeat: () => true);
    }
}