import axios from 'axios';
import config from '../../config.json'
import { Client } from './HttpClient';

export default function HttpClient() {
    return new Client(undefined, axios.create({
        baseURL: config.SERVER_URL,
        transformResponse: data => data 
    }));
}