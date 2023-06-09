import React from 'react';
import axios from 'axios';
import styled from "styled-components";

import Dropdown from './Components/Dropdown';
import Card from './Components/WeatherCard';
import Navbar from './Components/Navbar';
import CardList from './Components/CardList';
import './App.css'
import FavCityForm from './Components/FavCityForm';

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
      <h4>Server status: {data.status}</h4>
    </>
  ) : (
    <>
      <h3>Server status: no repsonse</h3>
    </>

  )
}

// function ApiStockholmWeather() {

//   const [weather, setWeather] = React.useState([]);

//   React.useEffect(() => {
//     const fetchData = async () => {
//       const result = await axios("https://localhost:40400/weatherdata");
//       console.log(result);
//       setWeather(result.data);
//     }
//     fetchData()

//     //console.log(data)
//   }, [])

//   return weather ? (

//     <p>the weather in stockholm is {weather.temp} celcius. </p>
//   ) : (
//     <>
//       <h1>weather data is loading</h1>
//     </>
//   )
// }

function App() {
  return (
    <>
      <Navbar>
        <h1>Rabbit Weather</h1>
      </Navbar>

      <p>Hello and welcome to the rabbit weather site.</p>
      <ApiCall />
      <h2>Current weather</h2>
      <CardList />
      <div>
        <h3>Add a favorite city here</h3>
        <FavCityForm />
      </div>

    </>
  )
}

export default App
