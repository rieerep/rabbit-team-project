import React, {useState, useEffect} from 'react';
import styled from 'styled-components';
import axios from 'axios';

import Dropdown from './Dropdown';
import CardList from './CardList';
import Card from './WeatherCard';
import { CardListContainer } from './CardList';

function FavCityForm() {
    const [FavCity, setFavCity] = useState({weather: []});
    const [city, setCity] = useState('');
    const [favSaved, setFavSaved] = useState([]);

    const handleCityChange = (evt) => {
        console.log(evt.target.value);
        setCity(evt.target.value);
    };

    const handleSubmit = (evt) => {
        evt.preventDefault();
        console.log(evt);
        const selectedCity = FavCity.weather.find((c) => c.name === city);
        if (selectedCity) {
            console.log('Added city:', selectedCity);
            setFavSaved(prevFavSaved => [...prevFavSaved, selectedCity]);
        }
    };

    useEffect(() => {
        const fetchData = async () => {
            try {
                const FavCityResult = await axios.get('http://dev.kjeldcon.se:20400/currentweather');
                console.log(FavCityResult);
                setFavCity(FavCityResult.data);
            } catch (error) {
                console.error('Error fetching data:', error);
            }
        };
        fetchData();
    }, []);

    
    return (
        <>
            <form onSubmit={handleSubmit}>
                <label>
                    Add a new favorite city
                    <select value={city} onChange={handleCityChange}>
                        <option value=""> - Please select a city -</option>
                        {FavCity.weather.map((city) => (
                            <option key={city.name} value={city.name}>
                                {city.name}
                            </option>
                        ))}
                    </select>
                </label>
                <button type="submit"> Save </button>
            </form>

            <h3>Current favourite cities:</h3>
            <div>
                <CardListContainer>
                    {favSaved.map((city) => (
                        <p key={city.name}><Card name={city.name} temp={city.temp} weather={city.weather}/></p>
                    ))}
                </CardListContainer>
            </div>
        </>
    )
}

export default FavCityForm;





// function FavouriteCity() {
//     const [FavCity, setFavCity] = React.useState([]);
//     const [city, setCity] = React.useState('');
//     const [favSaved, setFavSaved] = React.useState([]);

//     const handleCityChange = (evt) => {
//         console.log(evt.target.value);
//         setCity(evt.target.value);
//     };

//     const handleSubmit = (evt) => {
//         evt.preventDefault();
//         console.log(evt);
//         const selectedCity = FavCity.find((c) => c.name === city);
//         if (selectedCity) {
//             console.log('Added city:', selectedCity);
//             setFavSaved(prevFavSaved => [...prevFavSaved, selectedCity]);
//         }
//     };

//     React.useEffect(() => {
//         const fetchData = async () => {
//             try {
//                 const FavCityResult = await axios.get('https://localhost:40400/currentcities');
//                 console.log(FavCityResult);
//                 setFavCity(FavCityResult.data);
//             } catch (error) {
//                 console.error('Error fetching data:', error);
//             }
//         };
//         fetchData();
//     }, []);

    
//     return (
//         <>
//             <form onSubmit={handleSubmit}>
//                 <label>
//                     Add a new favorite city
//                     <select value={city} onChange={handleCityChange}>
//                         <option value=""> - Please select a city -</option>
//                         {FavCity.map((city) => (
//                             <option key={city.name} value={city.name}>
//                                 {city.name}
//                             </option>
//                         ))}
//                     </select>
//                 </label>
//                 <button type="submit"> Save </button>
//             </form>

//             <h3>Current favourite cities:</h3>
//             <div>
//                 <ul>
//                     {favSaved.map((city) => (
//                         <p key={city.name}>{city.name}</p>
//                     ))}
//                 </ul>
//             </div>
//         </>
//     )
// }