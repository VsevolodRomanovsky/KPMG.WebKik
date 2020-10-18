declare module ag.grid {
    const enum ColType {
        unknown,
        string,
        date,
        boolean,
        enum,
    }
    interface ColDef {
        type?: ColType;
        typeName?: string;
        namespace?: string;
    }
    class ValueGetterParams<T>{
        data: T;
        node: RowNode;
        colDef: ColDef;
        api: GridApi;
        context: any;
        getValue: Function;
    }
    class CellRenderParams<T> {
        value: any;
        data: T;
        colDef: ColDef;
        column: Column;
        $scope: any;
        rowIndex: number;
        eGridCell: any;
        api: GridApi;
        context: any;
        refreshCell: Function;
        valueGetter: Function;
        node: RowNode;
    }

    interface VHtmlElement {
        getElement(): any;

        setInnerHtml(innerHtml:string):void
        addStyles(styles);
        attachEventListeners(node);
        addClass(newClass);
        removeClass(oldClass)
        addClasses(classes) 
        toHtmlString():string; 
        toHtmlStringChildren():string 
        appendChild(child):string;
        setAttribute(key, value):void;
        addEventListener(event, listener);
        elementAttached (element) 
    }

    class SortModel {
        colId: string;
        sort: string;
    }

    class DataSourceParams {
        startRow: number;
        endRow: number;
        sortModel: SortModel[];
        filterModel: any;
        successCallback: (data: any[], lastRow?: number) => void;
        failCallback: () => void;
    }

    interface SetFilterParameters {
        apply?: boolean;
    }

    interface TextAndNumberFilterParameters {
        apply?: boolean;
    }

    interface FilterModel {
        filter: string;
        type: number;
    }

    interface FilterInitParams {
        colDef: ColDef;
        filterChangedCallback: Function;
        filterModifiedCallback: Function;
        doesRowPassOtherFilter: (node: RowNode) => boolean;
        filterParams: SetFilterParameters;
        context: any;
        $scope: any;
    }
    interface RowSelectedEvent {
        node: RowNode;
    }
}