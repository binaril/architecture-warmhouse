# Project_template

# Задание 1. Анализ и планирование

### 1. Описание функциональности монолитного приложения

**Управление отоплением:**

- Пользователи могут удалённо включать/выключать отопление в своих домах
- Система поддерживает управление системами отопления, установленных в домах

**Мониторинг температуры:**

- Пользователи могут просматривать текущую температуру в своих домах через веб-интерфейс
- Система поддерживает сбор данных с датчиков температур, установленных в домах

### 2. Анализ архитектуры монолитного приложения

- Язык программирования: Go
- База данных: PostgreSQL
- Архитектура: Монолитная, все компоненты системы (обработка запросов, бизнес-логика, работа с данными) находятся в рамках одного приложения.
- Взаимодействие: Синхронное, запросы обрабатываются последовательно.
- Масштабируемость: Ограничена, так как монолит сложно масштабировать по частям.
- Развертывание: Требует остановки всего приложения.

### 3. Определение доменов и границы контекстов

1. Домен «Отопление» 
    - получение и хранение данных с датчиков температур
    - управление регуляторами отопления, сохранение состояний
    - получение данных о работоспособности регуляторов
2. Домен "Умные устройства"
    - получение и хранение статусов устройств
    - удаленное управление устройствами (вкл/выкл)
    - получение данных о работоспособности устройств
3. Домен "Интеграция"
    - подключение новых домов и их умных устройств и модулей управления отоплением
4. Домен "Удаленное управление"
    - веб интерфейс для управления модулями отопления и умными устройствами
    - определение доступа к датчикам и устройствам
    - администрирование пользователей

### **4. Проблемы монолитного решения**

- Сложность разработки, высокий риск нарушить работу других компонентов приложения
- Длительность тестирования, требуется протестировать все приложение после каждого изменения
- Ограниченная масштабируемость (монолит придется масштабировать целиком, а не отдельные его компоненты)
- Для обновления любого компонента требуется перезапускать всего приложение
- Трудно работать над проектом большой командой


### 5. Визуализация контекста системы — диаграмма С4

[Context](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Context.png)

# Задание 2. Проектирование микросервисной архитектуры

**Диаграмма контейнеров (Containers)**

[Containers](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Containers.png)

**Диаграмма компонентов (Components)**

[Component.Automatic](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.Automatic.png)

[Component.DeviceControl](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.DeviceControl.png)

[Component.DeviceRepository](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.DeviceRepository.png)

[Component.GateWay](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.GateWay.png)

[Component.Notification](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.Notification.png)

[Component.Telemetry](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.Telemetry.png)

[Component.UserAccess](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.UserAccess.png)

[Component.WebApp](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.WebApp.png)

[Component.WebAppAdmin](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/C4.Component.WebAppAdmin.png)

**Диаграмма кода (Code)**

[Code.AutomaticEdit1.Sequence](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/Code.AutomaticEdit1.Sequence.png)

[Code.AutomaticEdit2.Sequence](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/Code.AutomaticEdit2.Sequence.png)

[Code.AutomaticScheduler.Sequence](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/Code.AutomaticScheduler.Sequence.png)

# Задание 3. Разработка ER-диаграммы

[ER](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/diagrams/images/ER.png)

# Задание 4. Создание и документирование API

### 1. Тип API

Для взаимодействия компонентов будет использоваться REST API. Так как большая часть взаимодействия подразумевает немедленное получение ответа (получить список устройств, получить список параметров и т.д.) то удобнее использовать синхронное взаимодействие

### 2. Документация API

[Swagger](https://github.com/binaril/architecture-warmhouse/blob/warmhouse/apps/swagger.yaml)

# Задание 5. Работа с docker и docker-compose

Перейдите в apps.

Там находится приложение-монолит для работы с датчиками температуры. В README.md описано как запустить решение.

Вам нужно:

1) сделать простое приложение temperature-api на любом удобном для вас языке программирования, которое при запросе /temperature?location= будет отдавать рандомное значение температуры.

Locations - название комнаты, sensorId - идентификатор названия комнаты

```
	// If no location is provided, use a default based on sensor ID
	if location == "" {
		switch sensorID {
		case "1":
			location = "Living Room"
		case "2":
			location = "Bedroom"
		case "3":
			location = "Kitchen"
		default:
			location = "Unknown"
		}
	}

	// If no sensor ID is provided, generate one based on location
	if sensorID == "" {
		switch location {
		case "Living Room":
			sensorID = "1"
		case "Bedroom":
			sensorID = "2"
		case "Kitchen":
			sensorID = "3"
		default:
			sensorID = "0"
		}
	}
```

2) Приложение следует упаковать в Docker и добавить в docker-compose. Порт по умолчанию должен быть 8081

3) Кроме того для smart_home приложения требуется база данных - добавьте в docker-compose файл настройки для запуска postgres с указанием скрипта инициализации ./smart_home/init.sql

Для проверки можно использовать Postman коллекцию smarthome-api.postman_collection.json и вызвать:

- Create Sensor
- Get All Sensors

Должно при каждом вызове отображаться разное значение температуры

Ревьюер будет проверять точно так же.


