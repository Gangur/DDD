import {  Alert, AlertTitle, Button, ButtonGroup, Container, List, ListItem, ListItemText, Typography } from "@mui/material";
import agent from "../../app/api/agent";
import { useState } from "react";
import { Link } from "react-router-dom";

export default function About() {
    const [validationErrors, setValidationErrors] = useState<string[]>([])

    function getValidationError() {
        agent.v1BuggyValidationProblem()
            .then(() => console.log('shold not see this'))
            .catch(error => setValidationErrors(error))
    }

    return (
        <Container>
            <Typography gutterBottom variant='h2'>Errors for testing purposes</Typography>
            <ButtonGroup fullWidth>
                <Button variant='contained' onClick={() => agent.v1BuggyBadRequest()}>Test 400 Bad Request Error</Button>
                <Button variant='contained' onClick={() => agent.v1BuggyUnauthorized()}>Test 401 Unauthorized Error</Button>
                <Button variant='contained' component={Link} to='/not-found' style={{ textAlign: 'center' }}>Test 404 Not Found Error</Button>
                <Button variant='contained' onClick={() => agent.v1BuggyServerError()}>Test 500 Server Error</Button>
                <Button variant='contained' onClick={getValidationError}>Test Validation Error</Button>
            </ButtonGroup>
            {validationErrors.length > 0 &&
                <Alert severity='error'>
                    <AlertTitle>Validation Errors</AlertTitle>
                    <List>
                        {validationErrors.map(error => (
                            <ListItem key={error}>
                                <ListItemText>{error}</ListItemText>
                            </ListItem>
                        ))}
                    </List>
                </Alert>}
        </Container>
    )
}