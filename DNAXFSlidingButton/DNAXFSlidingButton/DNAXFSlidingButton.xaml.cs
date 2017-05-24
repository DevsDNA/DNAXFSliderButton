namespace DNAXFSlidingButton
{
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public partial class DNAXFSlidingButton : ContentView
    {
        private bool actionActivated = false;

        public static readonly BindableProperty BackgroundContentProperty = BindableProperty.Create(nameof(BackgroundContent), typeof(ContentView), typeof(DNAXFSlidingButton), coerceValue: BackgroundContentCoerceValue);
        public static readonly BindableProperty IndicatorContentProperty = BindableProperty.Create(nameof(IndicatorContent), typeof(ContentView), typeof(DNAXFSlidingButton), coerceValue: IndicatorContentCoerceValue);
        public static readonly BindableProperty ThumbContentProperty = BindableProperty.Create(nameof(ThumbContent), typeof(ContentView), typeof(DNAXFSlidingButton), coerceValue: ThumbContentCoerceValue);
        public static readonly BindableProperty ActivateCommandProperty = BindableProperty.Create(nameof(ActivateCommand), typeof(ICommand), typeof(DNAXFSlidingButton), null);
        public static readonly BindableProperty ActivateCommandParameterProperty = BindableProperty.Create(nameof(ActivateCommandParameter), typeof(object), typeof(DNAXFSlidingButton), null);
        public static readonly BindableProperty AnimationSpeedProperty = BindableProperty.Create(nameof(AnimationSpeed), typeof(int), typeof(DNAXFSlidingButton), 500);

        public DNAXFSlidingButton()
        {
            InitializeComponent();
        }

        public ContentView BackgroundContent
        {
            get { return (ContentView)GetValue(BackgroundContentProperty); }
            set { SetValue(BackgroundContentProperty, value); }
        }

        public ContentView IndicatorContent
        {
            get { return (ContentView)GetValue(IndicatorContentProperty); }
            set { SetValue(IndicatorContentProperty, value); }
        }

        public ContentView ThumbContent
        {
            get { return (ContentView)GetValue(ThumbContentProperty); }
            set { SetValue(ThumbContentProperty, value); }
        }

        public ICommand ActivateCommand
        {
            get { return (ICommand)GetValue(ActivateCommandProperty); }
            set { SetValue(ActivateCommandProperty, value); }
        }

        public object ActivateCommandParameter
        {
            get { return GetValue(ActivateCommandParameterProperty); }
            set { SetValue(ActivateCommandParameterProperty, value); }
        }

        public int AnimationSpeed
        {
            get { return (int)GetValue(AnimationSpeedProperty); }
            set { SetValue(AnimationSpeedProperty, value); }
        }

        private static object BackgroundContentCoerceValue(BindableObject bindableObject, object value)
        {
            if (bindableObject != null && value != null && value is ContentView)
            {
                DNAXFSlidingButton instance = (DNAXFSlidingButton)bindableObject;
                instance.Background.Content = (ContentView)value;
            }
            return value;
        }

        private static object IndicatorContentCoerceValue(BindableObject bindableObject, object value)
        {
            if (bindableObject != null && value != null && value is ContentView)
            {
                DNAXFSlidingButton instance = (DNAXFSlidingButton)bindableObject;
                instance.ToShow.Content = (ContentView)value;
            }
            return value;
        }

        private static object ThumbContentCoerceValue(BindableObject bindableObject, object value)
        {
            if (bindableObject != null && value != null && value is ContentView)
            {
                DNAXFSlidingButton instance = (DNAXFSlidingButton)bindableObject;
                instance.ToSlide.Content = (ContentView)value;
            }
            return value;
        }

        private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (e.TotalX < 0 || (ActivateCommand != null && !ActivateCommand.CanExecute(ActivateCommandParameter)))
                return;

            if (e.StatusType == GestureStatus.Started)
            {
                actionActivated = false;
                ToShow.FadeTo(0, (uint)AnimationSpeed, Easing.CubicOut);
            }
            else if (e.StatusType != GestureStatus.Running)
            {
                ToShow.FadeTo(1, (uint)AnimationSpeed, Easing.CubicIn);
                ToSlide.TranslateTo(0, ToSlide.TranslationY, (uint)AnimationSpeed, Easing.CubicIn);
                PanPlaceholder.TranslateTo(0, ToSlide.TranslationY, (uint)AnimationSpeed, Easing.CubicIn);
                actionActivated = false;
            }

            TranslatContent(e.TotalX);
        }

        private void TranslatContent(double totalX)
        {
            double contentTranslation = Math.Round(totalX);

            if (contentTranslation < (ParentFrame.Width - ToSlide.Width))
            {
                ToSlide.TranslateTo(contentTranslation, ToSlide.TranslationY);
                PanPlaceholder.TranslateTo(contentTranslation, ToSlide.TranslationY);
            }
            else if ((ToSlide.TranslationX + ToSlide.Width) >= (ParentFrame.Width - ToSlide.Width) && !actionActivated)
            {
                actionActivated = true;
                if (ActivateCommand != null)
                {
                    ActivateCommand.Execute(ActivateCommandParameter);
                }
            }
        }
    }
}
