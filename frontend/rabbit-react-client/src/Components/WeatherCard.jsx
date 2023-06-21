import styled from "styled-components";

const CardContainer = styled.div`
display: flex;
flex-direction: column;
//border: 2px solid red;
padding: 10px;
border-radius: 10px;
background-color: orangered;
//box-shadow: 0px 0px 5px 10px gray;
min-width: 90px;
`;

function Card(props) {

    return(
        <CardContainer>
            <h4>{props.name}</h4>
            <b>{props.temp}</b>
            <p>{props.weather}</p>
        </CardContainer>
    );
}
export default Card;

//export default WeatherCard