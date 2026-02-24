
const BASE_URL = "https://wttr.in";

const formatWeather = (data, city) => `
Weather Report
-------------------------
City: ${city}
Temperature: ${data.current_condition[0].temp_C}°C
Feels Like: ${data.current_condition[0].FeelsLikeC}°C
Humidity: ${data.current_condition[0].humidity}%
Condition: ${data.current_condition[0].weatherDesc[0].value}
`;

export const fetchWeatherWithPromises = (city) => {
  return fetch(`${BASE_URL}/${city}?format=j1`)
    .then(response => {
      if (!response.ok) {
        throw new Error("City not found or API error");
      }
      return response.json();
    })
    .then(data => {
      console.log(formatWeather(data, city));
    })
    .catch(error => {
      console.error("❌ Error:", error.message);
    });
};


export const fetchWeatherAsync = async (city) => {
  try {
    const response = await fetch(`${BASE_URL}/${city}?format=j1`);

    if (!response.ok) {
      throw new Error("City not found or API error");
    }

    const data = await response.json();

    console.log(formatWeather(data, city));

  } catch (error) {
    console.error("❌ Error:", error.message);
  }
};