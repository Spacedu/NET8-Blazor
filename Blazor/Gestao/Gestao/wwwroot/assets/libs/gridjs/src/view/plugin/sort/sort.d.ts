import { h } from 'preact';
import { Comparator, TCell, TColumnSort } from '../../../types';
export interface SortConfig {
    compare?: Comparator<TCell>;
}
export interface GenericSortConfig {
    multiColumn?: boolean;
    server?: {
        url?: (prevUrl: string, columns: TColumnSort[]) => string;
        body?: (prevBody: BodyInit, columns: TColumnSort[]) => BodyInit;
    };
}
export declare function Sort(props: {
    index: number;
} & SortConfig): h.JSX.Element;
