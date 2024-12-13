import { FormControl, FormControlLabel, Radio, RadioGroup } from "@mui/material";

interface Props {
    options: any[];
    onChange: (event: any) => void;
    selectedValue: string;
}

export default function RadiooButtonGroup({options, onChange, selectedValue}: Props) {
    return <FormControl>
        <RadioGroup onChange={onChange} value={selectedValue}>
            {options.map(({ name }) => (
                <FormControlLabel key={name} value={name} control={<Radio />} label={name} />
            ))}
        </RadioGroup>
    </FormControl>
}