import { ComponentChild } from 'preact';
import Row from './row';
import { SortConfig } from './view/plugin/sort/sort';
import { JSXInternal } from 'preact/src/jsx';
import { Plugin } from './plugin';
export type OneDArray<T> = T[];
export type TwoDArray<T> = T[][];
export type TCell = number | string | boolean | ComponentChild | HTMLElement;
export type TDataArrayRow = OneDArray<TCell>;
export type TDataArray = OneDArray<TDataArrayRow>;
export type TDataObjectRow = {
    [key: string]: TCell;
};
export type TDataObject = OneDArray<TDataObjectRow>;
export type TData = TDataArray | TDataObject;
export interface TColumn {
    id?: string;
    data?: ((row: TDataArrayRow | TDataObjectRow) => TCell) | TCell;
    name?: string | ComponentChild;
    plugin?: Plugin<any>;
    width?: string;
    minWidth?: string;
    sort?: SortConfig;
    columns?: OneDArray<TColumn>;
    resizable?: boolean;
    hidden?: boolean;
    formatter?: (cell: TCell, row: Row, column: TColumn) => ComponentChild;
    attributes?: ((cell: TCell | null, row: Row | null, column: TColumn) => JSXInternal.HTMLAttributes<HTMLTableCellElement>) | JSXInternal.HTMLAttributes<HTMLTableCellElement>;
}
export type Comparator<T> = (a: T, b: T) => number;
export interface TColumnSort {
    index: number;
    direction?: 1 | -1;
}
export declare enum Status {
    Init = 0,
    Loading = 1,
    Loaded = 2,
    Rendered = 3,
    Error = 4
}
export type CSSDeclaration = {
    [key: string]: string | number;
};
