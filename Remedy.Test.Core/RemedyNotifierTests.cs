namespace Remedy.Test.Core
{
    using System.Diagnostics;

    using Remedy.Core;

    public class RemedyNotifierTests
    {
        [Fact]
        public void DifferentScoreValues_Should_FireOnPropertyChangedAction()
        {
            var isScoreChanged = false;
            var notifier = new NotifierObject
            {
                Score = 1,
                OnScorePropertyChanged = () =>
                {
                    Debug.WriteLine($"{nameof(NotifierObject.Score)} property changed.");
                    isScoreChanged = true;
                },
                ValidateScoreForChange = (backingStore, value) =>
                {
                    return backingStore != value && value > 0;
                }
            };

            isScoreChanged = false;
            notifier.Score = 3;

            isScoreChanged.Should()
                .BeTrue();
            notifier.Score.Should()
                .Be(3);
        }

        [Fact]
        public void SameIdValues_Should_NotFireOnPropertyChanged()
        {
            var isIdChanged = false;
            var notifier = new NotifierObject
            {
                Id = 1,
                OnIdPropertyChanged = () => isIdChanged = true
            };

            isIdChanged = false;
            notifier.Id = 1;

            isIdChanged.Should()
                .BeFalse();
            notifier.Id.Should()
                .Be(1);
        }

        [Fact]
        public void SameLastNameValues_Should_NotFireOnPropertyChangedAction()
        {
            var lastName = "Park";
            var isLastNameChanged = false;
            var notifier = new NotifierObject
            {
                LastName = lastName,
                OnLastNamePropertyChanged = () =>
                {
                    Debug.WriteLine($"{nameof(NotifierObject.LastName)} property changed.");
                    isLastNameChanged = true;
                },
                Score = 10,
                ValidateScoreForChange = (backingStore, value) =>
                {
                    return backingStore != value && value > 0;
                }
            };

            isLastNameChanged = false;
            notifier.LastName = lastName;

            isLastNameChanged.Should()
                .BeFalse();
            notifier.LastName.Should()
                .BeSameAs(lastName);
        }

        [Fact]
        public void SameOrDifferentValuesForFirstName_Should_FireNotifyPropertyChanged()
        {
            var isFirstNameChanged = false;
            var notifier = new NotifierObject
            {
                FirstName = "Sam",
                OnFirstNamePropertyChanged = () => isFirstNameChanged = true
            };

            isFirstNameChanged = false;
            notifier.FirstName = "Sam";
            isFirstNameChanged.Should()
                .BeTrue();
            notifier.FirstName.Should()
                .BeSameAs("Sam");

            isFirstNameChanged = false;
            notifier.FirstName = "Lucy";
            isFirstNameChanged.Should()
                .BeTrue();
            notifier.FirstName.Should()
                .BeSameAs("Lucy");
        }

        [Fact]
        public void SamePriceValues_Should_NotFireOnPropertyChanged()
        {
            var isPriceChanged = false;
            var notifier = new NotifierObject
            {
                Price = 21.0,
                OnIdPropertyChanged = () => isPriceChanged = true
            };

            isPriceChanged = false;
            notifier.Price = 21.0;

            isPriceChanged.Should()
                .BeFalse();
            notifier.Price.Should()
                .Be(21.0);
        }

        [Fact]
        public void SameScoreValues_Should_NotFireOnPropertyChangedAction()
        {
            var score = 1;
            var isScoreChanged = false;
            var notifier = new NotifierObject
            {
                Score = score,
                OnScorePropertyChanged = () =>
                {
                    Debug.WriteLine($"{nameof(NotifierObject.Score)} property changed.");
                    isScoreChanged = true;
                },
                ValidateScoreForChange = (backingStore, value) =>
                {
                    return backingStore != value && value > 0;
                }
            };

            isScoreChanged = false;
            notifier.Score = score;

            isScoreChanged.Should()
                .BeFalse();
            notifier.Score.Should()
                .Be(score);
        }

        [Fact]
        public void ScoreValueLessThanZero_Should_NotFireOnPropertyChangedAction()
        {
            var isScoreChanged = false;
            var notifier = new NotifierObject
            {
                Score = 1,
                OnScorePropertyChanged = () =>
                {
                    Debug.WriteLine($"{nameof(NotifierObject.Score)} property changed.");
                    isScoreChanged = true;
                },
                ValidateScoreForChange = (backingStore, value) =>
                {
                    return backingStore != value && value > 0;
                }
            };

            notifier.Score = 0;
            isScoreChanged.Should()
                .BeFalse();
            notifier.Score.Should()
                .Be(1);
        }

        public class NotifierObject : RemedyNotifier
        {
            private string _firstName;
            private int _id;
            private string _lastName;
            private double _price;
            private int _score;

            public string FirstName
            {
                get => _firstName;
                set
                {
                    _firstName = value;
                    TellPropertyChanged(() => FirstName);
                    OnFirstNamePropertyChanged?.Invoke();
                }
            }

            public int Id
            {
                get => _id;
                set
                {
                    if (SetProperty(ref _id, value))
                        OnIdPropertyChanged?.Invoke();
                }
            }

            public string LastName
            {
                get => _lastName;
                set => SetProperty(ref _lastName, value,
                    OnLastNamePropertyChanged);
            }

            public Action OnFirstNamePropertyChanged { get; set; }
            public Action OnIdPropertyChanged { get; set; }
            public Action OnLastNamePropertyChanged { get; set; }

            public Action OnPriceChanged { get; set; }
            public Action OnScorePropertyChanged { get; set; }

            public double Price
            {
                get => _price;
                set
                {
                    if (SetProperty(ref _price, value, () => Price))
                    {
                        OnPriceChanged?.Invoke();
                    }
                }
            }

            public int Score
            {
                get => _score;
                set => SetProperty(ref _score, value,
                    OnScorePropertyChanged, ValidateScoreForChange);
            }

            public Func<int, int, bool> ValidateScoreForChange { get; set; }
        }
    }
}