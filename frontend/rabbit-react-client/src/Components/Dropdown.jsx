import React, { useState, useEffect } from "react";

import styled from "styled-components";
import axios from "axios";

const Dropdown = () => {

    const [data, setData] = useState({ weather: [] });

    useEffect(() => {
        const fetchData = async () => {
            const result = await axios("https://localhost:40400/currentweather");
            console.log(result);
            setData(result.data);
        }
        fetchData()

        //console.log(data)
    }, [])


    return (
        <>
            <label>
                City:
            </label>
            <select >
                <option value="">Select a city</option>
                {data.weather.map(cities => (<option >{cities.name}</option>))}
            </select>
        </>
    )
}

export default Dropdown;