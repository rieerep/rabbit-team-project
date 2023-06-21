import React from 'react';
import axios from 'axios';

function FavouriteCity() {
    const [FavCity, setFavCity] = React.useState([]);
    const [city, setCity] = React.useState('');
    const [favSaved, setFavSaved] = React.useState([]);

    const handleCityChange = (evt) => {
        console.log(evt.target.value);
        setCity(evt.target.value);
    };

    const handleSubmit = (evt) => {
        evt.preventDefault();
        console.log(evt);
        const selectedCity = FavCity.find((c) => c.name === city);
        if (selectedCity) {
            console.log('Added city:', selectedCity);
            setFavSaved(prevFavSaved => [...prevFavSaved, selectedCity]);
        }
    };

    React.useEffect(() => {
        const fetchData = async () => {
            try {
                const FavCityResult = await axios.get('https://localhost:40400/currentcities');
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
                        {FavCity.map((city) => (
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
                <ul>
                    {favSaved.map((city) => (
                        <p key={city.name}>{city.name}</p>
                    ))}
                </ul>
            </div>
        </>
    )
}

export default FavouriteCity;
