import { Comparator, TCell } from '../../../types';
export declare const SortColumn: (index: number, direction: 1 | -1, multi?: boolean, compare?: Comparator<TCell>) => (state: any) => any;
export declare const SortToggle: (index: number, multi: boolean, compare?: Comparator<TCell>) => (state: any) => any;
