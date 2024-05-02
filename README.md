
# Игра Ludum Dare: Froust!

"Froust!" - это игра, созданная в рамках хакатона Ludum Dare. В этой игре вы воплощаетесь в роль снеговика, который обитает на льдине в снежном царстве. Ваша цель - продержаться как можно дольше на льдине, сталкивая снеговиков противников со льдины.  Геймплей можно посмотреть по [ссылке](https://ebrietas.itch.io/froust)

## Содержание

1. [Управление](#управление)
2. [Цель этого репозитория](#цель-этого-репозитория)
3. [Технологии](#технологии)
4. [Инструкции по установке и запуску](#инструкции-по-установке-и-запуску)
5. [Структура проекта](#структура-папок-проекта)
6. [Основные точки входа](#основные-точки-входа)
7. [Скриншоты](#скриншоты)
8. [Участие](#участие)

## Управление

Вы можете управлять снеговиком с помощью клавиатуры или экранного джойстика. Используйте клавиши стрелок для перемещения снеговика по льдине. Если вы используете экранный джойстик, просто коснитесь и проведите пальцем по экрану в нужном направлении для перемещения снеговика.

## Цель этого репозитория

Цель этого репозитория - предоставить примеры моего кода и подходов к разработке игр. Здесь вы найдете исходный код игры "Froust!" и примеры использования различных технологий и библиотек для разработки игр на платформе Unity. Мой опыт, представленный здесь, может быть полезным для других разработчиков, и я надеюсь, что вы найдете этот репозиторий полезным и вдохновляющим для своих собственных проектов.

## Технологии

- **[VContainer](https://github.com/hadashiA/VContainer)**: VContainer - это легковесный контейнер внедрения зависимостей для Unity. Он облегчает управление зависимостями и обеспечивает инверсию управления в вашем проекте. 
В проекте используется как основной фреймворк организующий взаимодействия вне основного геймплея.

- **[LeoECSLite](https://github.com/Leopotam/ecslite)**: LeoECSLite - это легковесная и эффективная библиотека для реализации Entity-Component-System (ECS) в Unity. Она обеспечивает высокую производительность и удобство в использовании для организации логики и управления игровыми объектами.
 В проекте LeoECSLite применяется для организации геймплейного процесса на уровне.

- **[Unitask](https://github.com/Cysharp/UniTask)**: Unitask - это простая и мощная библиотека для управления асинхронными операциями в Unity. Она облегчает выполнение асинхронных задач, таких как загрузка ресурсов, анимации и т. д., и обеспечивает чистый и удобный синтаксис. 
В проекте Unitask используется для асинхронной загрузки ресурсов из Addressables и асинхронного переключения состояний.

- **[UniRx](https://github.com/neuecc/UniRx)**: UniRx (Reactive Extensions for Unity) использовался для реализации реактивного программирования в игре. Это позволяет управлять асинхронными событиями и реакциями на них в удобном и эффективном стиле.
В проекте используется для реактивного доступа к некоторым данным игрового процесса

- **Addressables**: Addressables - это мощный инструмент для управления ресурсами в проектах Unity. Он позволяет загружать, разгружать и управлять ресурсами, такими как текстуры, модели, аудиофайлы и другие, с использованием уникальных идентификаторов.
В проекте Addressables используется для хранения игровых ресурсов.

- **DoTween**: DoTween - это библиотека для создания анимаций в Unity. Она предоставляет мощные инструменты для создания разнообразных анимаций, включая движение, вращение, изменение цвета и многое другое. 
В проекте DoTween используется для некоторых анимаций

## Инструкции по установке и запуску
Для создания использовалась **Unity  2022.3.8f1**.
1. Клонируйте репозиторий с помощью `git clone`.
2. Откройте проект. 
3. Убедитесь, что все зависимости правильно настроены и импортированы.
4. Откройте сцену **/_Project/Scenes/MainScene**
5. Запустите проект и начните игру!

## Структура папок проекта

- **`Assets`**: Папка содержит все ресурсы игры, такие как сцены, префабы, текстуры, аудиофайлы и скрипты.
  - **`_Project`**: Основная папка проекта.
	  - **`Audio`**: Аудио библиотеки содержащие ссылки на соответствующие аудиофайлы.
	  - **`Configs`**: Основные конфигурационные файлы относящиеся к геймплею
	  - **`Installers`**: Здесь расположены основные инсталлеры дополнительных пакетов.
	  - **`Modules`**: Дополнительные модули, сервисы и расширения
	  - **`Prefabs`**: Здесь расположены все префабы относящиеся к игре
	   	  - **`Level`**: Все префабы относящиеся к геймплею
		  - **`UI`**: Все префабы относящиеся к UI. Экраны и их элементы
		  - **`Vfx`**: Все префабы эффектов.
	  - **`Scenes`**: Здесь расположена основная игровая сцена.
	  - **`Scripts`**: Основная кодовая база.
	  - **`Sources`**: Все медиа ресурсы.
		  - **`Animation`**: Настройки анимаций и аниматоров
		  - **`Audio`**: Все аудиофайлы
		  - **`Fonts`**: Шрифты. *Нужно оптимизировать*
		  - **`Materials`**: Материалы игровых моделей, эффектов
		  - **`Models`**: Модели использующиеся в игре
		  - **`Textures`**: Текстуры и спрайты
		  
## Основные точки входа
- **[`/Scripts/Runtime/Foundation/Scopes/ProjectLifetimeScope.cs`](https://github.com/Slemura/Froust/blob/main/Assets/_Project/Scripts/Runtime/Foundation/Scopes/ProjectLifetimeScope.cs)**: Основной VContainer скоуп в котором описываются используемые модули и части логики.
- **[`/Scripts/Runtime/Foundation/EntryPoint/AppCore.cs`](https://github.com/Slemura/Froust/blob/main/Assets/_Project/Scripts/Runtime/Foundation/EntryPoint/AppCore.cs)**: Ядро окружения в котором инициализируется машина состояний и описываются отношения между состояниями.
- **[`/Scripts/Runtime/Foundation/EntryPoint/States/`](https://github.com/Slemura/Froust/tree/main/Assets/_Project/Scripts/Runtime/Foundation/EntryPoint/States)***: Список состояний описывающий весь игровой процесс
- **[`/Scripts/Runtime/Gameplay/GameplayEcsStartup.cs`](https://github.com/Slemura/Froust/blob/main/Assets/_Project/Scripts/Runtime/Gameplay/GameplayEcsStartup.cs)***: Обертка над ECS для создание геймплейной логики

## Скриншоты

![gameplay](https://i.imgur.com/902XD0G.png)![gameplay 2](https://i.imgur.com/AqXHt9r.png)

## Участие

Этот проект был создан в рамках хакатона Ludum Dare. Благодарим организаторов и всех участников за возможность создать эту игру!