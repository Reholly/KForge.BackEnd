using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Education;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("/api/edu/lecture")]
public class LectureController
{
    //Создать лекцию
    //Получить все лекции курса
    //Получить конкретную лекцию курс
    //Обновить лекцию (редактирование)
    //Удалить лекцию 
    //Пройти лекцию (отметить прочитанной у юзера)
}