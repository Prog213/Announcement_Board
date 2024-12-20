# Announcement Board

## Video Link
https://drive.google.com/file/d/1z0TCj5-2eIKEo-gntPHyQ1uQqX6QMpTO/view?usp=sharing

## Деплой проєкту (окремо API та RazorPages)
### Використання Visual Studio: 
- Відкриваємо проєкт у Visual Studio.
- Вибераємо Publish: Azure > Azure App Service (Windows/Linux).
- Входимо в обліковий запис та публікуємо проект
### Додавання конфігурацій
- Додаємо BaseAddress для Azure Application Settings (RazorPages) для налаштування базової адреси API
- Додаємо AllowedOrigin для Azure Application Settings (API) для налаштування CORS
### Azure SQL Database:
- Створюємо Azure SQL Database для SQLServer
- Додаємо правила для firewall для нашого проекту
- Копіюємо Connection String з налаштувань Azure
- Додаємо Connection String у Web API App Service
