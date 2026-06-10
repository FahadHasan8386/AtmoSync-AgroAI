# 🌱 AtmoSync AI

## 🚀 Overview

AtmoSync AI continuously monitors environmental conditions by collecting data from multiple sensors and automatically controls irrigation based on humidity levels. Historical sensor data is analyzed using Machine Learning models to predict environmental trends and support smarter decision-making.

### 🎯 Problem Statement

Traditional monitoring systems only display current sensor readings and require manual intervention for irrigation. AtmoSync AI addresses this challenge by:

* Monitoring environmental conditions in real time
* Automating irrigation through relay-controlled water pumps
* Predicting future environmental conditions using AI
* Providing centralized visualization and analytics

---

## ✨ Features

### 📡 Real-Time Environmental Monitoring

* Temperature Monitoring (DHT22)
* Humidity Monitoring (DHT22)
* Carbon Monoxide Detection (MQ-7)
* Hydrogen Sulfide Detection (MQ-136)
* Live Sensor Status Tracking

### 💧 Smart Irrigation System

* Automatic Water Pump Control
* Humidity-Based Irrigation Logic
* Relay Module Integration
* Real-Time Pump Status Monitoring

### 🤖 AI-Powered Analytics

* Environmental Trend Forecasting
* Air Quality Prediction
* Historical Data Analysis
* Machine Learning Insights

### 📊 Interactive Dashboard

* Real-Time Sensor Visualization
* Historical Data Reports
* Prediction Results
* Device Health Monitoring
* Responsive UI Design

---

## 🏗️ System Architecture

ESP32 Sensors

⬇

ASP.NET Core Web API

⬇

SQL Server Database

⬇

Python Machine Learning Engine

⬇

Blazor WebAssembly Dashboard

---

## 🛠 Technology Stack

### Hardware

* ESP32
* DHT22 Temperature & Humidity Sensor
* MQ-7 Carbon Monoxide Sensor
* MQ-136 Hydrogen Sulfide Sensor
* Relay Module
* Water Pump

### Backend

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server

### Frontend

* Blazor WebAssembly
* Bootstrap 5

### AI & Machine Learning

* Python
* Scikit-Learn
* Pandas
* NumPy

### Database

* SQL Server

---

## 🔄 Workflow

1. ESP32 collects environmental sensor data.
2. Sensor data is transmitted to ASP.NET Core Web API.
3. Data is stored in SQL Server.
4. Python ML models process historical data.
5. Predictions are generated and stored.
6. Blazor Dashboard displays live readings and predictions.
7. Irrigation system automatically activates when humidity drops below the configured threshold.

---

## 📈 Future Enhancements

* Weather API Integration
* SMS & Email Alerts
* Mobile Application
* Multi-Device Management
* Advanced Deep Learning Models
* Cloud Deployment
* Solar-Powered Operation

---

## 🎯 Project Goals

* Improve environmental awareness
* Automate irrigation processes
* Reduce water wastage
* Provide predictive environmental insights
* Demonstrate integration of IoT, Web Development, Database Systems, and Artificial Intelligence

---

## 👨‍💻 Developed By

**Fahad Hasan**

Full Stack Developer | Competitive Programmer | IoT & AI Enthusiast

*"Synchronizing Environmental Data with Artificial Intelligence."* 🚀
