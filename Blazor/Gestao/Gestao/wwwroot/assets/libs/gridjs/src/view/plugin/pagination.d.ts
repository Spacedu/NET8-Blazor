import { h } from 'preact';
export interface PaginationConfig {
    limit?: number;
    page?: number;
    summary?: boolean;
    nextButton?: boolean;
    prevButton?: boolean;
    buttonsCount?: number;
    resetPageOnUpdate?: boolean;
    server?: {
        url?: (prevUrl: string, page: number, limit: number) => string;
        body?: (prevBody: BodyInit, page: number, limit: number) => BodyInit;
    };
}
export declare function Pagination(): h.JSX.Element;
