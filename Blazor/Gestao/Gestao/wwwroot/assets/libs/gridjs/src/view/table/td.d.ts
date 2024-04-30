import { h, JSX } from 'preact';
import Cell from '../../cell';
import { CSSDeclaration, TColumn } from '../../types';
import Row from '../../row';
export declare function TD(props: {
    cell: Cell;
    row?: Row;
    column?: TColumn;
    style?: CSSDeclaration;
    messageCell?: boolean;
} & Omit<JSX.HTMLAttributes<HTMLTableCellElement>, 'style'>): h.JSX.Element;
