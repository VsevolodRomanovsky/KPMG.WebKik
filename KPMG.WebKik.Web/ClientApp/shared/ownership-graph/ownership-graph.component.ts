import { Component, ElementRef, Input, OnInit, Renderer } from '@angular/core';
import * as d3 from "d3";

import { OwnershipGraphChart } from './ownership-graph-chart';
import { OwnershipGraphNode } from './ownership-graph-node';
import { OwnershipGraphLink } from './ownership-graph-link';
import { OwnershipGraphConfig } from './ownership-graph-config';

@Component({
    selector: 'ownership-graph',
    template: require('./ownership-graph.component.html'),
    styles: [require('./ownership-graph.component.scss')]
})
export class OwnershipGraphComponent implements OnInit {
    @Input() nodes: Array<{ Id: number, DisplayName: string }>;
    @Input() links: Array<{ SourceId: number, TargetId: number, Share: number }>;

    constructor(private element: ElementRef, private renderer: Renderer) {

    }

    ngOnInit() {
        const svg = this.getSvg();
        const config = new OwnershipGraphConfig(svg);
        const data = this.getChartData(config);
        const chart = new OwnershipGraphChart(svg, data.nodes, data.links, config);
        chart.create();
    }

    getChartData(config: OwnershipGraphConfig): { nodes: Array<OwnershipGraphNode>, links: Array<OwnershipGraphLink> } {
        const nodes: Array<OwnershipGraphNode> = this.nodes.map(node => {
            return new OwnershipGraphNode(config, node.Id, node.DisplayName);
        });
        const links: Array<OwnershipGraphLink> = this.links.map(link => {
            const source = nodes.find(node => { return node.id == link.SourceId });
            const target = nodes.find(node => { return node.id == link.TargetId });
            return new OwnershipGraphLink(source, target, link.Share);
        });
        return { nodes: nodes, links: links };
    }

    getSvg(): any {
        return d3.select(this.element.nativeElement).select('svg');
    }
}