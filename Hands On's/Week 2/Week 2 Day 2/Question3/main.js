import { fetchWeatherWithPromises, fetchWeatherAsync } from "./WeatherApp.js";

// Using Promises
fetchWeatherWithPromises("Pune");
fetchWeatherWithPromises("Jalgaon");

// Using Async/Await
fetchWeatherAsync("Mumbai");