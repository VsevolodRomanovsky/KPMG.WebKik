export class OwnershipGraphConfig {
    public width: number;
    public height: number;

    constructor(svg: any) {
        const svgRect = svg.node().getBoundingClientRect();
        this.width = svgRect.width;
        this.height = svgRect.height;
        
    }

    get rectWidth() { return this.width / 6; }
    get rectHeight() { return this.height / 6; }
    get minDimension() { return Math.min(this.width, this.height); }
    get linkDistance() { return this.minDimension / 10; }
}