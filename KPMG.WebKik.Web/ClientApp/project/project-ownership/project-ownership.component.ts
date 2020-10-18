import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ProjectService } from '../project.service';
import { ProjectOwnershipViewModel } from '../model/project-ownership.model';

@Component({
    template: require('./project-ownership.component.html'),
    styles: [require('./project-ownership.component.scss')]
})
export class ProjectOwnershipComponent implements OnInit {
    @ViewChild('ownershipGraph') ownershipGraph;

    projectId: number;
    ownership: ProjectOwnershipViewModel;

    isLoading: boolean;

    constructor(private projectService: ProjectService, private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.isLoading = true;
        this.projectId = parseInt(this.route.snapshot.params['projectid'], 10);
        this.projectService.getProjectOwnership(this.projectId).then(result => {
            this.ownership = result;
            this.isLoading = false;
        });
    }

    SaveAsPng() {
        const self = this;
        const svg = this.ownershipGraph.element.nativeElement.querySelector('svg');
        const svgXml = (new XMLSerializer()).serializeToString(svg);

        var image = new Image();
        image.onload = function () {
            const canvas = document.createElement('CANVAS') as any;
            const context = canvas.getContext("2d");
            canvas.setAttribute('width', svg.clientWidth);
            canvas.setAttribute('height', svg.clientHeight);
            context.drawImage(image, 0, 0);
            const canvasData = canvas.toDataURL("image/png");
            const a = document.createElement("a");
            a.textContent = "Сохранение";
            a.download = "Граф владения " + self.ownership.Project.Name + ".png";
            a.href = canvasData;
            document.body.appendChild(a);
            a.addEventListener('click', () => {
                a.parentNode.removeChild(a);
            });
            a.click();
        };
        image.src = 'data:image/svg+xml;base64,' + this.b64EncodeUnicode(svgXml);
    }

    b64EncodeUnicode(str) {
        return btoa(encodeURIComponent(str).replace(/%([0-9A-F]{2})/g, function (match, p1) {
            return String.fromCharCode(parseInt('0x' + p1, 16));
        }));
    }
}

