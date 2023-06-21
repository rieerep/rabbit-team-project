import React, {useState, useEffect} from 'react';
import styled from 'styled-components';
import axios from 'axios';

import Dropdown from './Dropdown';
import CardList from './CardList';

function FavCityForm() {
    const [data, setData] = useState({weather: []});

    useEffect(() => {
        const fetchData = async () => {
            const result = await axios( "https://localhost:40400/currentweather" );

            console.log(result);
            console.log("LOOK HERE")
            setData(result.data);
        };

        fetchData();
    }, []);

    function HandleSubmit(evt) {
        console.log("evt")
    }

    return(
        <>
        <form onSubmit={HandleSubmit}>
            <label>
                City:
            </label>
            <select >
                <option value="">Select a city</option>
                {data.weather.map(cities => (<option >{cities.name}</option>))}
            </select>
            <button type="submit">Save</button>
        </form>
        </>
    );
}

export default FavCityForm;