import { Category } from "../api/http-client";

export interface ProductParams {
    orderBy: string;
    searchTerm?: string;
    categories?: Category[];
    brands?: string[];
    pageNumber: number;
    pageSize: number;
    descending: boolean
}