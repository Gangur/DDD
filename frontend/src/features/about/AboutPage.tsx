import {  Alert, AlertTitle, Button, ButtonGroup, Container, List, ListItem, ListItemText, Typography } from "@mui/material";
import agent from "../../app/api/agent";
import { useEffect, useState } from "react";
import { AgGridReact } from "ag-grid-react";
import { AllCommunityModule, ColDef, ColGroupDef, ModuleRegistry } from 'ag-grid-community'; 
import { CustomerDto } from "../../app/api/http-client";
import { useLazyGet400ErrorQuery, useLazyGet401ErrorQuery, useLazyGet404ErrorQuery, useLazyGet500ErrorQuery, useLazyGetValidationErrorQuery } from "./errorApi";

export default function About() {
    const [trigger400Error] = useLazyGet400ErrorQuery();
    const [trigger401Error] = useLazyGet401ErrorQuery();
    const [trigger404Error] = useLazyGet404ErrorQuery();
    const [trigger500Error] = useLazyGet500ErrorQuery();
    const [triggerValidateionError] = useLazyGetValidationErrorQuery();
    const [validationErrors, setValidationErrors] = useState<string[]>([])

    useEffect(() => {
        agent.customeres.list('name', true, 1, 50)
            .then(c => setRowData(c.values))
            .catch(error => setValidationErrors(error));
    }, []);

    const [rowData, setRowData] = useState<CustomerDto[] | undefined>([])

    
    const [colDefs] = useState<(ColDef<any> | ColGroupDef<any>)[] | null>([
        { field: "id", flex: 1 },
        { field: "email", flex: 2 },
        { field: "name", flex: 2 }
    ]);

    // Register all Community features
    ModuleRegistry.registerModules([AllCommunityModule]);

    return (
        <Container>
            <Typography gutterBottom variant='h2'>Errors for testing purposes</Typography>
            <ButtonGroup fullWidth>
                <Button variant='contained' onClick={() => trigger400Error()}>Test 400 Bad Request Error</Button>
                <Button variant='contained' onClick={() => trigger401Error()}>Test 401 Unauthorized Error</Button>
                <Button variant='contained' onClick={() => trigger404Error()} style={{ textAlign: 'center' }}>Test 404 Not Found Error</Button>
                <Button variant='contained' onClick={() => trigger500Error()}>Test 500 Server Error</Button>
                <Button variant='contained' onClick={() => triggerValidateionError()}>Test Validation Error</Button>
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