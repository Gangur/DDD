import axios from 'axios';
import config from '../../../config.json'
import { Client } from './http-client';

axios.defaults.baseURL = config.SERVER_URL;

const agent = new Client(undefined, axios.create({
    baseURL: config.SERVER_URL,
    transformResponse: data => data
}));

export default agent;