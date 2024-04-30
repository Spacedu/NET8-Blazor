import { h } from 'preact';
import { TCell } from '../../../types';
export interface SearchConfig {
    keyword?: string;
    ignoreHiddenColumns?: boolean;
    debounceTimeout?: number;
    selector?: (cell: TCell, rowIndex: number, cellIndex: number) => string;
    server?: {
        url?: (prevUrl: string, keyword: string) => string;
        body?: (prevBody: BodyInit, keyword: string) => BodyInit;
    };
}
export declare function Search(): h.JSX.Element;
