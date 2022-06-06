# Тестовое задание в DNS (C# + Selenium WebDriver + NUnit)
Реализован параллельный запуск автотестов браузере Chrome. Добавлены параметризованные тесты.
#### Подробное описание UI-автотестов
* ***SearchCityByPressSearchButtonTest***

  *Шаги выполнения:*
  - Открыть страницу
  - Нажать на кнопку с выбором города
  - Ввести город в поле поиска
  - Нажать на левую кнопку поиска
  - Проверить, что город поменялся на ранее введенный
 
* ***SearchCityByPressEnterKeyTest***

  *Шаги выполнения:* 
  - Открыть страницу 
  - Нажать на кнопку с выбором города
  - Ввести город в поле поиска
  - Нажать на клавишу Enter
  - Проверить, что город поменялся на ранее введенный

* ***ClearingSearchFieldTest***

  *Шаги выполнения:*
  - Открыть страницу 
  - Нажать на кнопку с выбором города
  - Ввести город в поле поиска
  - Нажать на кнопку очистки поля поиска

* ***ClosingPageTest***

  *Шаги выполнения:*
  - Открыть страницу 
  - Нажать на кнопку с выбором города
  - Нажать на кнопку закрытия страницы "Выбор города"
  - Проверить, что страницы "Выбор города" нет в открытой вкладке браузера

* ***ListPopularCitiesTest***

  *Шаги выполнения:*
  - Открыть страницу 
  - Нажать на кнопку с выбором города
  - Извлечь список популярных городов из страницы "Выбор города"
  - Проверить, что список популярных городов совпадает с ожидаемым

* ***SelectCityFromListTest*** 

  *Шаги выполнения:*
  - Открыть страницу 
  - Нажать на кнопку с выбором города
  - Выбрать из выпадающего списка округ, регион и город
  - Проверить, что город поменялся на выбранный

* ***ListDistrictsTest*** 

  *Шаги выполнения:*
  - Открыть страницу 
  - Нажать на кнопку с выбором города
  - Проверить, что список округов совпадает с ожидаемым

* ***CityAfterSearchAndPageRefreshTest*** 

  *Шаги выполнения:*
  - Открыть страницу
  - Нажать на кнопку с выбором города
  - Ввести город в поле поиска
  - Нажать на левую кнопку поиска
  - Обновить страницу
  - Проверить, что город поменялся на ранее введенный
