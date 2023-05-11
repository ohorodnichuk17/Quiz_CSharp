using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

using QuizLibrary.Entities;
using QuizLibrary.Security;
using QuizLibrary.Stores;
using QuizLibrary.Stores.Operations;

namespace QuizApi
{
    public sealed class Store
    {
        private static GeneralStore _instance;

        public GeneralStore Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GeneralStore(new JsonLoader("quiz", "accounts", "results"));

                    _instance.AccountStore_ = _instance.Loader.LoadAccountStore() ?? GenerateAccountStore(new JsonSaver("accounts"));
                    _instance.ResultStore_ = _instance.Loader.LoadResultStore() ?? GenerateResultStore(new JsonSaver("results"));
                    _instance.QuizStore_ = _instance.Loader.LoadQuizStore() ?? GenerateQuizStore(new JsonSaver("quiz"));
                    QuizLibrary.Entities.Quiz.IdInc = (from quiz in _instance.QuizStore_.Quiz select quiz.Id).Max() + 1;

                    _instance.AccountStore_.Saver = new JsonSaver("accounts");
                    _instance.QuizStore_.Saver = new JsonSaver("quiz");
                    _instance.ResultStore_.Saver = new JsonSaver("results");
                }
                return _instance;
            }
        }

        ////////////////////////////////////////////////////

        private AccountStore GenerateAccountStore(ISaveAccountStore saver)
        {
            var accounts = new AccountStore();
            accounts.Saver = saver;
            accounts.Add(new QuizLibrary.Entities.Account("root", "toor", new DateTime(1970, 1, 1)));
            return accounts;
        }

        private ResultStore GenerateResultStore(ISaveResultStore saver)
        {
            var results = new ResultStore();
            results.Saver = saver;
            results.Add(new Result("oleg", "History", 1, 15));
            results.Add(new Result("misha", "Geography", 1, 7));
            results.Add(new Result("dasha", "Maths", 1, 5));
            results.Add(new Result("vika", "Geography", null, 13));
            return results;
        }

        private QuizStore GenerateQuizStore(ISaveQuizStore saver)
        {
            var quizStore = new QuizStore();
            quizStore.Saver = saver;
            var quiz = new QuizLibrary.Entities.Quiz
            {
                Name = "Стародавній світ та Середні віки",
                AuthorLogin = "root",
                Sections =
                {
                    new Section
                    {
                        Name = "History",
                        Questions =
                        {
                            new Question {
                                Text = "Столиця Стародавнього Єгипту",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Вавилон", false), new Answer("Мемфіс", true), new Answer("Каїр", false) }
                            },
                            new Question {
                                Text = "Як називаються гробниці фараонів Стародавнього Єгипту",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("храми", false), new Answer("піраміди", true), new Answer("катакомбы", false) }
                            },
                            new Question {
                                Text = "Кому належала верховна влада у Стародавньому Єгипті",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("фараону", true), new Answer("вельможам", false), new Answer("жерцям", false) }
                            },
                            new Question {
                                Text = "Якому фараонові була побудована найбільша піраміда",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Хеопсу", true), new Answer("Хефрену", false), new Answer("Джосеру", false) }
                            },
                            new Question {
                                Text = "Як називали бога Сонця – наймогутнішого бога Стародавнього Єгипту",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Осіріс", false), new Answer("Амон-Ра", true), new Answer("Анубіс", false) }
                            },
                            new Question {
                                Text = "Як звали дружину фараона Ехнатона, скульптура якої досі є символом краси, гармонії та природності",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Хатшепсут", false), new Answer("Нефертіті", true), new Answer("Клеопатра", false) }
                            },
                            new Question {
                                Text = "Який давньогрецький історик назвав Єгипет «дар Нілу»",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Аристотель", false), new Answer("Тацит", false), new Answer("Геродот", true) }
                            },
                            new Question {
                                Text = "Якому богу були присвячені Олімпійські ігри у Стародавній Греції:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Зевсу", true), new Answer("Аполлону", false), new Answer("Посейдону", false) }
                            },
                            new Question {
                                Text = "У якого бога стародавні греки просили успішного плавання кораблю:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Зевса", false), new Answer("Аполлона", false), new Answer("Посейдона", true) }
                            },
                            new Question {
                                Text = "Народження грецького театру пов'язане зі святами на честь бога:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Зевса", false), new Answer("Діоніса", true), new Answer("Посейдона", false) }
                            },
                            new Question {
                                Text = "Автором поем «Іліада» та «Одіссея» був давньогрецький поет:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Эсхіл", false), new Answer("Гомер", true), new Answer("Аристофан", false) }
                            },
                            new Question {
                                Text = "Який знаменитий храм грецького архітектора знаходиться на вершині афінського Акрополя:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("зіккурат", false), new Answer("Парфенон", true), new Answer("Пантеон", false) }
                            },
                            new Question {
                                Text = "За легендою, братів Ромула та Рема – засновників Стародавнього Риму, вигодувала:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("лиса", false), new Answer("вовчиця", true), new Answer("медведиця", false) }
                            },
                            new Question {
                                Text = "Стародавній Рим розташовувався:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("на Пиренійському півострові", false),
                                    new Answer("на Балканському півострові", false), new Answer("на Апеннінському півострові", true) }
                            },
                            new Question {
                                Text = "Як називається величезний амфітеатр Риму, де боролися гладіатори і в якому могло бути до 50 тисяч глядачів:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Колзей", true), new Answer("Пантеон", false), new Answer("Форум", false) }
                            },
                            new Question {
                                Text = "На честь військових перемог полководців у Стародавньому Римі споруджували:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("храми", false), new Answer("обеліски", false), new Answer("триумфальні арки", true) }
                            },
                            new Question {
                                Text = "Як називався народ, який здавна населяв Англію:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("брити", true), new Answer("англи", false), new Answer("нормани", false) }
                            },
                            new Question {
                                Text = "До якої королівської династії належав Карл Великий:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("Меровінги", true), new Answer("Каролінги", false), new Answer("Бурбони", false) }
                            },
                            new Question {
                                Text = "Кого в середні віки називали лицарем:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("королівських наближених", false), new Answer("великих феодалів", false),
                                    new Answer("власників маєтків, які несли військову службу", true) }
                            },
                            new Question {
                                Text = "Як у середні віки називалося земельне володіння, за яке несли військову службу:",
                                IsSeveralCorrectAnswers = false,
                                Answers = { new Answer("оброк", false), new Answer("феод", true), new Answer("титул", false) }
                            },
                        }
                    }
                }
            };
            quizStore.Add(quiz);
            return quizStore;
        }
    }
}