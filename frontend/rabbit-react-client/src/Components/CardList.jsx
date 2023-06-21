import React, {useState, useEffect} from 'react';
import styled from 'styled-components';
import axios from 'axios';

import Card from './WeatherCard';

export const CardListContainer = styled.div`
    display: flex;
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: space-around;
    align-items: center;
`;

export const GET_ALL_WEATHER_DATA = "http://dev.kjeldcon.se:20400/currentweather";

function CardList() {

    const [data, setData] = useState({weather: []});

    useEffect(() => {
        const fetchData = async () => {
            const result = await axios( GET_ALL_WEATHER_DATA );

            console.log(result);
            setData(result.data);
        };

        fetchData();
    }, []);

    return(
        <CardListContainer>
            {data.weather.map(city => (<Card name={city.name} temp={city.temp} weather={city.weather}/>))}
            
        </CardListContainer>
    );
}

export default CardList;