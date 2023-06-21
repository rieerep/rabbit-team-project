import React from 'react';
import axios from 'axios';

import './App.css'
import * as css from './css-components/css-app'
import FavouriteCity from './FavouriteCity'

  function clickMe() {
     alert("Button clicked!");
   }

function ApiCall() {
  const [data, setData] = React.useState([]);

  React.useEffect(() => {
    const fetchData = async () => {
      const result = await axios("https://localhost:40400/healthcheck");

      console.log(result);

      setData(result.data);
    }
    fetchData()

    //console.log(data)
  }, [])

  return data ? (
    <>
      <h3>
        {data.status}
        </h3>
    </>
  ) : (
    <>
      <h3>loading repsonse</h3>
    </>

  )
}

function ApiStockholmWeather() {
  const [weather, setWeather] = React.useState([]);

  React.useEffect(() => {
    const fetchData = async () => {
      const result = await axios("https://localhost:40400/weatherdata");

      console.log(result);

      setWeather(result.data);

    }
    fetchData()

    //console.log(data)
  }, [])

  return weather ? (
    <p>the weather in stockholm is {weather.temp} celcius</p>
  ) : (
    <>
      <h1>weather data is loading</h1>
    </>
  )
}

function  App() {

  return (
    <>
      <div>
        <h1>Ridiculously Rabbid Rabbit Weather</h1>
        <p>Hello and welcome to the rabbit weather site.</p>
        <ApiCall />
      </div>
      <div>
        <button onClick={clickMe}>
          Button
        </button>
      </div>
      <div>
        <h3>Stockholm weather:</h3>
        <ApiStockholmWeather />
      </div>
      <css.Favcity>
        <FavouriteCity/>
      </css.Favcity>
    </>
  ) 
}

export default App
