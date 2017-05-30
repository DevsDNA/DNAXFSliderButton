# DNAXFSliderButton
This control allow you to create an sliding button, much like the shutdown button on iOS or the old unlock button on iOS.

The control is completely written in XAML and C# and don't uses renders, so it is available on iOS, Android and Windows.


![SliderButton](/screenshots/screenshot.gif?raw=true "Slider button cross platform")

## How to use the control.
Just include the code (or compile it) add a XMLNS to the control namespace:

`xmlns:control="clr-namespace:DNAXFSlidingButton;assembly=DNAXFSlidingButton"`

And you are ready to go:

```
<control:DNAXFSlidingButton HorizontalOptions="Center" VerticalOptions="Center" HeightRequest="44" WidthRequest="150" 
                            ActivateCommand="{Binding TestCommand}" AnimationSpeed="800">
    <control:DNAXFSlidingButton.BackgroundContent>
        <ContentView>
            <!-- Background showed when sliding the thumb 
                    using the AnimationSpeed property -->
        </ContentView>
    </control:DNAXFSlidingButton.BackgroundContent>
    <control:DNAXFSlidingButton.IndicatorContent>
        <ContentView>
            <!-- Content to show when not sliding the thumb -->
        </ContentView>
    </control:DNAXFSlidingButton.IndicatorContent>
    <control:DNAXFSlidingButton.ThumbContent>
        <ContentView  WidthRequest="40" HeightRequest="40" HorizontalOptions="Start" VerticalOptions="Center">
            <!-- Content to show for the thumb -->
        </ContentView>
    </control:DNAXFSlidingButton.ThumbContent>
</control:DNAXFSlidingButton>
```
