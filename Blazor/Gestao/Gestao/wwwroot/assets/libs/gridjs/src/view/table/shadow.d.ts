import { h } from 'preact';
export declare function ShadowTable(props: {
    tableRef: HTMLTableElement;
}): h.JSX.Element;
export declare function getShadowTableWidths(tempRef: HTMLDivElement): {
    [columnId: string]: {
        minWidth: number;
        width: number;
    };
};
