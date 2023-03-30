using FitnessClub.DAL.FitnessClubDataBase.Entities.Consumers;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dbo;
using FitnessClub.DAL.FitnessClubDataBase.Entities.Dictionaries;

namespace FitnessClub.DAL.FitnessClubDataBase.FakeData;

public static class Initializer
{
    public static async Task Initialize(this FitnessClubContext fitnessClubContext)
    {
        if (!fitnessClubContext.Database.EnsureCreated())
            return;

        var userRoles = new List<UserRole> {
            new UserRole { Code = UserRole.CLIENT, Description = "Клиент" },
            new UserRole { Code = UserRole.TRAINER, Description = "Тренер" },
        };

        await fitnessClubContext.UserRoles.AddRangeAsync(userRoles);

        var requstStatuses = new List<RequestStatus> {
            new RequestStatus { Code = RequestStatus.CREATED, Description = "Создана" },
            new RequestStatus { Code = RequestStatus.IN_PROGRESS, Description = "В обработке" },
            new RequestStatus { Code = RequestStatus.REJECTED, Description = "Отклонена" },
            new RequestStatus { Code = RequestStatus.COMPLETED, Description = "Выполнена" }
        };

        await fitnessClubContext.RequestStatuses.AddRangeAsync(requstStatuses);

        var exercises = new List<Exercise> {
            new Exercise { Code = Exercise.SQUATTING_ON_TOES, Description = "Приседание на носочках" },
            new Exercise { Code = Exercise.PUSH_UP, Description = "Отжимания" },
            new Exercise { Code = Exercise.SHOULDER_ROTATION, Description = "Вращение плечами" },
            new Exercise { Code = Exercise.REVERSAL_OF_HANDS, Description = "Разворот рук" },
            new Exercise { Code = Exercise.HULL_SLOPES, Description = "Наклоны корпуса" }
        };

        await fitnessClubContext.Exercises.AddRangeAsync(exercises);
        await fitnessClubContext.SaveChangesAsync();

        var users = new List<User> {
            new User {
                FullName = "Фирзов Анатолий Андреевич",
                BirthDay = DateTime.Now.AddYears(-22),
                Gender = "Мужской",
                PhoneNumber = "test1",
                Password = "1",
                UserRole = userRoles[0]
            },
            new User {
                FullName = "Абрамова Кристина Алексеевна",
                BirthDay = DateTime.Now.AddYears(-19),
                Gender = "Женский",
                PhoneNumber = "test2",
                Password = "password",
                UserRole = userRoles[0]
            },
            new User {
                FullName = "Афисова Юлия Владимировна",
                BirthDay = DateTime.Now.AddYears(-24),
                Gender = "Женский",
                PhoneNumber = "test3",
                Password = "3",
                UserRole = userRoles[1]
            },
        };

        await fitnessClubContext.Users.AddRangeAsync(users);
        await fitnessClubContext.SaveChangesAsync();

        var requests = new List<Request> {
            new Request {
                Title = "Заявка на тренеровки по йоге",
                Porpose = "По совету жены хочу начать заниматься йогой для улучшения здоровья.",
                RequestStatus = requstStatuses[1],
                UserClient = users[0],
                UserManager = users[2],
                ModifiedOn = DateTime.Now.AddMinutes(1),
            },
            new Request {
                Title = "Быстрое похудение",
                Porpose = "Хочу к лето походеть на 3 кг, а дальше контролироваться себя.",
                RequestStatus = requstStatuses[3],
                UserClient = users[1],
                UserManager = users[2],
                ModifiedOn = DateTime.Now.AddMinutes(3),
            }
        };

        await fitnessClubContext.Requests.AddRangeAsync(requests);
        await fitnessClubContext.SaveChangesAsync();

        var individualPlans = new List<IndividualPlan> {
            new IndividualPlan {
                StartedOn = DateTime.Now,
                EndedOn = DateTime.Now.AddDays(7),
                Cost = 10_000m,
                Request = requests[0]
            },
            new IndividualPlan {
                StartedOn = DateTime.Now.AddDays(8),
                EndedOn = DateTime.Now.AddDays(15),
                Cost = 5_000m,
                Request = requests[0]
            },
            new IndividualPlan {
                StartedOn = DateTime.Now.AddDays(1),
                EndedOn = DateTime.Now.AddDays(3),
                Cost = 2_500m,
                Request = requests[1]
            },
            new IndividualPlan {
                StartedOn = DateTime.Now.AddDays(6),
                EndedOn = DateTime.Now.AddDays(8),
                Cost = 3_000m,
                Request = requests[1]
            }
        };

        await fitnessClubContext.IndividualPlans.AddRangeAsync(individualPlans);
        await fitnessClubContext.SaveChangesAsync();

        var workouts = new List<Workout> {
            new Workout {
                NumberOfRepetitions = 20,
                QuantityPerWeek = 7,
                AssignedOn = DateTime.Now,
                IsDone = true,
                Pulse = 89,
                IndividualPlan = individualPlans[0],
                Exercises = new List<Exercise> {
                    exercises[2],
                    exercises[4]
                }
            },
            new Workout {
                NumberOfRepetitions = 20,
                QuantityPerWeek = 7,
                AssignedOn = DateTime.Now.AddDays(3),
                IsDone = false,
                IndividualPlan = individualPlans[0],
                Exercises = new List<Exercise> {
                    exercises[1],
                    exercises[3],
                    exercises[0],
                }
            },
            new Workout {
                NumberOfRepetitions = 10,
                QuantityPerWeek = 7,
                AssignedOn = DateTime.Now.AddDays(4),
                IsDone = false,
                IndividualPlan = individualPlans[0],
                Exercises = new List<Exercise> {
                    exercises[4]
                }
            },
            new Workout {
                NumberOfRepetitions = 30,
                QuantityPerWeek = 7,
                AssignedOn = DateTime.Now.AddDays(8),
                IsDone = false,
                IndividualPlan = individualPlans[1],
                Exercises = new List<Exercise> {
                    exercises[3]
                }
            },
            new Workout {
                NumberOfRepetitions = 20,
                QuantityPerWeek = 3,
                AssignedOn = DateTime.Now.AddDays(1),
                IsDone = false,
                IndividualPlan = individualPlans[2],
                Exercises = new List<Exercise> {
                    exercises[1],
                    exercises[2]
                }
            },
            new Workout {
                NumberOfRepetitions = 30,
                QuantityPerWeek = 3,
                AssignedOn = DateTime.Now.AddDays(6),
                IsDone = false,
                IndividualPlan = individualPlans[3],
                Exercises = new List<Exercise> {
                    exercises[0]
                }
            },
        };

        await fitnessClubContext.Workouts.AddRangeAsync(workouts);
        await fitnessClubContext.SaveChangesAsync();
    }
}
