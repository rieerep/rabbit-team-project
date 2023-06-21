import styled from "styled-components";

const CardContainer = styled.div`
display: flex;
flex-direction: column;
border: 2px solid red;
padding: 10px;
border-radius: 10px;
background-color: blueviolet;

`;

function Card(props) {

    return(
        <CardContainer>
            <h4>{props.name}</h4>
            <p>{props.weather}</p>
        </CardContainer>
    );
}
export default Card;

//export default WeatherCard