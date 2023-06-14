import React, { useState, useEffect } from 'react';
import axios from 'axios';

import './App.css'

function ApiCall() {
  const [data, setData] = useState([]);

  useEffect(() => {
    const fetchData = async () => {
      const result = await axios("http://localhost:20400/healthcheck");
      console.log(result);
      setData(result.data);
    }
    fetchData()

    //console.log(data)
  }, [])
};

function App() {

  return (
    <>
      <div>
        <h1>Ridiculously Rabbid Rabbit Weather</h1>
        <ApiCall />
      </div>
    </>
  )
}

export default App
