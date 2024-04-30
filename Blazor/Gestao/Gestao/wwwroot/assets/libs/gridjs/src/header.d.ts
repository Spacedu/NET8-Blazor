import { OneDArray, TColumn, TwoDArray } from './types';
import Base from './base';
import { Config } from './config';
import { ComponentChild, RefObject } from 'preact';
declare class Header extends Base {
    private _columns;
    constructor();
    get columns(): OneDArray<TColumn>;
    set columns(columns: OneDArray<TColumn>);
    get visibleColumns(): OneDArray<TColumn>;
    adjustWidth(config: Config, tableRef: RefObject<HTMLTableElement>, tempRef: RefObject<HTMLDivElement>): this;
    private setSort;
    private setResizable;
    private setID;
    private populatePlugins;
    static fromColumns(columns: OneDArray<TColumn | string | ComponentChild>): Header;
    static createFromConfig(config: Partial<Config>): Header | null;
    static fromHTMLTable(element: HTMLElement): Header;
    static tabularFormat(columns: OneDArray<TColumn>): TwoDArray<TColumn>;
    static leafColumns(columns: OneDArray<TColumn>): OneDArray<TColumn>;
    static maximumDepth(column: TColumn): number;
}
export default Header;
