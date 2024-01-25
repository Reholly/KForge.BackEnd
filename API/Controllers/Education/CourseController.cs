using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Education;

[ApiController]
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("/api/edu/course")]
public class CourseController
{
    //Создать курс
    //Удалить курс
    //Получить все курсы
    //Получить курсы по тегу\названию (реквест поисковой)
    //Редактировать курс (название и описание
    //Добавить теги \ удалить теги (обновить теги для курса)
    
    //Создать секцию
    //удалить секцию
    //редактировать секцию (название)
    //Прикрепить к секции таск \ лекцию
    //Открепить от секции таск \ лекцию
    //Поменять порядок в секции - (дто список с порядковыми номерами новыми)
    
}