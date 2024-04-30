import { h, JSX } from 'preact';
import { CSSDeclaration, TColumn } from '../../types';
export declare function TH(props: {
    index: number;
    column: TColumn;
    style?: CSSDeclaration;
} & Omit<JSX.HTMLAttributes<HTMLTableCellElement>, 'style'>): h.JSX.Element;
