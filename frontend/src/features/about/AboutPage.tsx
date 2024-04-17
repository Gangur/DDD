import {  Button, ButtonGroup, Container, Typography } from "@mui/material";
import agent from "../../app/api/agent";

export default function About() {
    return (
        <Container>
            <Typography gutterBottom variant='h2'>Errors for testing purposes</Typography>
            <ButtonGroup fullWidth>
                <Button variant='contained' onClick={() => agent.v1BuggyBadRequest()}>Test 400 Bad Request Error</Button>
                <Button variant='contained' onClick={() => agent.v1BuggyUnauthorized()}>Test 401 Unauthorized Error</Button>
                <Button variant='contained' onClick={() => agent.v1BuggyNotFound()}>Test 404 Not Found Error</Button>
                <Button variant='contained' onClick={() => agent.v1BuggyServerError()}>Test 500 Server Error</Button>
                <Button variant='contained' onClick={() => agent.v1BuggyValidationProblem()}>Test Validation Error</Button>
            </ButtonGroup>
        </Container>
    )
}