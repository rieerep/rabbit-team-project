import React from 'react';
import axios from 'axios';
import styled from "styled-components";

import Dropdown from './Components/Dropdown';
import WeatherCard from './Components/WeatherCard';
import Navbar from './Components/Navbar';
import './App.css'

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

    <p>the weather in stockholm is {weather.temp} celcius. </p>
  ) : (
    <>
      <h1>weather data is loading</h1>
    </>
  )
}

function App() {
  return (
    <>
      <Navbar>
        <h1>Rabbit Weather</h1>
      </Navbar>

      <p>Hello and welcome to the rabbit weather site.</p>
      <ApiCall />
      <div>
        <Dropdown></Dropdown>
        <button onClick={clickMe}>
          Button
        </button>
      </div>
      <WeatherCard>
        <h3>Stockholm weather:</h3>
        <ApiStockholmWeather />
      </WeatherCard>

    </>
  )
}

export default App
