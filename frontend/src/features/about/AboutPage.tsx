import {  Alert, AlertTitle, Button, ButtonGroup, Container, List, ListItem, ListItemText, Typography } from "@mui/material";
import agent from "../../app/api/agent";
import { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import { AgGridReact } from "ag-grid-react";
import { AllCommunityModule, ModuleRegistry } from 'ag-grid-community'; 
import { CustomerDto } from "../../app/api/http-client";

export default function About() {
    const [validationErrors, setValidationErrors] = useState<string[]>([])

    useEffect(() => {
        agent.customeres.list('name', true, 1, 50)
            .then(c => setRowData(c.values))
            .catch(error => setValidationErrors(error));
    }, []);

    const [rowData, setRowData] = useState<CustomerDto[] | undefined>([])

    const [colDefs] = useState([
        { field: "id", flex: 1 },
        { field: "email", flex: 2 },
        { field: "name", flex: 2 }
    ]);

    // Register all Community features
    ModuleRegistry.registerModules([AllCommunityModule]);

    function getValidationError() {
        agent.buggy.validationProblem()
            .then(() => console.log('shold not see this'))
            .catch(error => setValidationErrors(error))
    }

    return (
        <Container>
            <Typography gutterBottom variant='h2'>Errors for testing purposes</Typography>
            <ButtonGroup fullWidth>
                <Button variant='contained' onClick={() => agent.buggy.badRequest()}>Test 400 Bad Request Error</Button>
                <Button variant='contained' onClick={() => agent.buggy.unauthorized()}>Test 401 Unauthorized Error</Button>
                <Button variant='contained' component={Link} to='/not-found' style={{ textAlign: 'center' }}>Test 404 Not Found Error</Button>
                <Button variant='contained' onClick={() => agent.buggy.serverError()}>Test 500 Server Error</Button>
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
                <div style={{ height: 500, marginTop: 30 }}>
                    <AgGridReact
                        rowData={rowData}
                        columnDefs={colDefs}
                    />
                </div>
        </Container>
    )
}