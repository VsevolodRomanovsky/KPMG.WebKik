import { OwnershipGraphNode } from './ownership-graph-node';
import { OwnershipGraphLink } from './ownership-graph-link';
import { OwnershipGraphConfig } from './ownership-graph-config';
import * as d3 from "d3";

export
    class OwnershipGraphChart {
    private simulation: any;
    private nodeElements: any;
    private linkElements: any;
    private linkTextElements: any;

    constructor(private svg: any,
        private nodes: Array<OwnershipGraphNode>,
        private links: Array<OwnershipGraphLink>,
        private config: OwnershipGraphConfig) {
    }

    public create() {
        this.createSimulation();
        this.defineArrows();
        this.createNodeElements();
        this.createLinkElements();
        this.createLinkTextElements();
    }

    onTick() {
        this.linkElements.attr('d', function (d) {
            return 'M' + d.sourceX + ',' + d.sourceY + 'L' + d.targetX + ',' + d.targetY;
        });

        this.nodeElements.attr('transform', function (d) {
            return 'translate(' + d.x + ',' + d.y + ')';
        });

        this.linkTextElements
            .attr('transform', function (d) {
                const x = d.sourceX + (d.targetX - d.sourceX) / 2;
                const y = d.sourceY + (d.targetY - d.sourceY) / 2;
                return 'translate(' + x + ',' + y + ')';
            });
    }

    dragstarted(node) {
        if (!d3.event.active) this.simulation.alphaTarget(0.3).restart();
        node.fx = node.x;
        node.fy = node.y;
    }

    dragged(node) {
        node.fx = d3.event.x;
        node.fy = d3.event.y;
    }

    dragended(node) {
        if (!d3.event.active) this.simulation.alphaTarget(0);
    }

    dblclick(node) {
        node.fx = null;
        node.fy = null;
    }

    defineArrows() {
        this.svg.append('svg:defs').append('svg:marker')
            .attr('id', 'end-arrow')
            .attr('viewBox', '0 -5 10 10')
            .attr('refX', 10)
            .attr('markerWidth', 5)
            .attr('markerHeight', 5)
            .attr('orient', 'auto')
            .append('svg:path')
            .attr('d', 'M0,-5L10,0L0,5');
    }

    createSimulation() {
        this.simulation = d3.forceSimulation()
            .force('link', d3.forceLink().distance(this.config.linkDistance))
            .force('charge', d3.forceManyBody())
            .force('centerX', d3.forceX(this.config.width / 2))
            .force('centerY', d3.forceY(this.config.height / 2));

        this.simulation
            .force('link')
            .links(this.links);

        this.simulation
            .nodes(this.nodes)
            .on('tick', this.onTick.bind(this));
    }

    createNodeElements() {
        this.nodeElements = this.svg
            .append('svg:g')
            .selectAll('g')
            .data(this.nodes)
            .enter()
            .append('svg:g');

        this.nodeElements
            .append('svg:rect')
            .attr("class", "node-rectangle")
            .attr('width', this.config.rectWidth)
            .attr('height', this.config.rectHeight)

        this.nodeElements
            .append('svg:text')
            .attr('x', 0)
            .attr('y', 0)
            .attr('dy', 0)
            .text(function (d) { return d.displayName; })
            .call(this.wrap.bind(this), this.config.rectWidth);

        this.nodeElements
            .on("dblclick", this.dblclick.bind(this))
            .call(d3.drag()
                .on('start', this.dragstarted.bind(this))
                .on('drag', this.dragged.bind(this))
                .on('end', this.dragended.bind(this)));

    }

    createLinkElements() {
        this.linkElements = this.svg
            .append('svg:g')
            .selectAll('path')
            .data(this.links)
            .enter()
            .append('svg:path')
            .attr('class', 'link')
            .style('marker-end', function (d) { return 'url(#end-arrow)'; })
    }

    createLinkTextElements() {
        this.linkTextElements = this.svg
            .append('svg:g')
            .selectAll('g')
            .data(this.links)
            .enter()
            .append('svg:g');

        this.linkTextElements
            .append('svg:text')
            .attr('class', 'link-text')
            .text(function (d) { return d.share > 100 ? '100+%' : Math.round(d.share) + '%'; });
    }

    wrap(text, width) {
        const halfWidth = this.config.rectWidth / 2;
        const rectHeight = this.config.rectHeight;

        text.each(function () {
            let text = d3.select(this),
                words = text.text().split(/\s+/).reverse(),
                word,
                line = [],
                lineNumber = 0,
                y = text.attr("y"),
                tspan = text.text(null).append("tspan").attr("x", halfWidth).attr("y", y);
            while (word = words.pop()) {
                line.push(word);
                tspan.text(line.join(" "));
                if (tspan.node().getComputedTextLength() > width) {
                    line.pop();
                    tspan.text(line.join(" "));
                    line = [word];
                    tspan = text
                        .append("tspan")
                        .attr("x", halfWidth)
                        .attr("y", y)
                        .text(word);
                }
            }

            tspan = d3.select(this).selectAll('tspan');
            const lineHeight = tspan.node().getBBox().height;
            const wrappedTextHeight = lineHeight * tspan.size();
            const dy = (rectHeight - wrappedTextHeight) / 2 + lineHeight * 4 / 5;
            tspan.each(function (d, i) {
                let item = d3.select(this);
                item.attr('dy', i * lineHeight + dy + 'px');
            });

        });
    }
}
