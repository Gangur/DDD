import { Category } from "../api/http-client";

export interface ProductParams {
    orderBy: string;
    searchTerm?: string;
    category?: Category;
    brand?: string;
    pageNumber: number;
    pageSize: number;
    descending: boolean
}