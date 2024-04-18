
export interface CustomerDto {
    id?: string;
    email?: string | undefined;
    name?: string | undefined;
}

export interface CustomerDtoIReadOnlyCollectionResult {
    success?: boolean;
    errorMessage?: string | undefined;
    value?: CustomerDto[] | undefined;
}

export interface CustomerDtoResult {
    success?: boolean;
    errorMessage?: string | undefined;
    value?: CustomerDto;
}

export interface GuidResult {
    success?: boolean;
    errorMessage?: string | undefined;
    value?: string;
}

export interface OrderDto {
    id?: string;
    customerId?: string;
}

export interface OrderDtoIReadOnlyCollectionResult {
    success?: boolean;
    errorMessage?: string | undefined;
    value?: OrderDto[] | undefined;
}

export interface OrderDtoResult {
    success?: boolean;
    errorMessage?: string | undefined;
    value?: OrderDto;
}

export interface ProductDto {
    id?: string;
    name?: string | undefined;
    pictureName?: string | undefined;
    priceCurrency?: string | undefined;
    priceAmount?: number;
    sku?: string | undefined;
}

export interface ProductDtoIReadOnlyCollectionResult {
    success?: boolean;
    errorMessage?: string | undefined;
    value?: ProductDto[] | undefined;
}

export interface ProductDtoResult {
    success?: boolean;
    errorMessage?: string | undefined;
    value?: ProductDto;
}

export interface Result {
    success?: boolean;
    errorMessage?: string | undefined;
}

export interface Body {
    file: string;

    [key: string]: any;
}

export interface FileParameter {
    data: any;
    fileName: string;
}
