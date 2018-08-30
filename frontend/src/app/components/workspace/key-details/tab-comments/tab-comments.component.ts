import { Component, OnInit, Input, SimpleChanges, ElementRef, ViewChild } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ComplexStringService } from '../../../../services/complex-string.service';
import { AppStateService } from '../../../../services/app-state.service';
import { Validators, FormGroup, FormBuilder } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Comment } from '../../../../models/comment';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import * as signalR from '@aspnet/signalr';
import { environment } from '../../../../../environments/environment';
import { SignalrService } from '../../../../services/signalr.service';
import { CommaExpr } from '@angular/compiler';

@Component({
    selector: 'app-tab-comments',
    templateUrl: './tab-comments.component.html',
    styleUrls: ['./tab-comments.component.sass']
})
export class TabCommentsComponent implements OnInit {

    @Input() comments: Comment[];
    @ViewChild('textarea') textarea: ElementRef;

    newComment: any;
    routeSub: Subscription;
    keyId: number;
    private url: string = environment.apiUrl;
    commentForm = this.fb.group({
        commentBody: ['',]
    });
    body: string;

    constructor(private userService: UserService,
        private fb: FormBuilder,
        private complexStringService: ComplexStringService,
        private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private activatedRoute: ActivatedRoute) { }


    ngOnInit() {
        this.routeSub = this.activatedRoute.params.subscribe((params) => {
            this.keyId = params.keyId;
        });
    }

    ngOnChanges(changes: SimpleChanges) {
        this.commentForm.reset();
    }

    ngOnDestroy() {
        this.routeSub.unsubscribe();
    }

    onImageClick(avatarUrl: string) {
        if (avatarUrl) {
            let dialogRef = this.dialog.open(ImgDialogComponent, {
                data: {
                    imageUri: avatarUrl
                }
            });
        }
    }

    public addComment(commentBody: string) {
        this.newComment = {
            user: this.userService.getCurrentUser(),
            text: commentBody,
            createdOn: new Date(Date.now())
        };

        this.complexStringService.createStringComment(this.newComment, this.keyId)
            .subscribe(
                (comments) => {
                    if (comments) {
                        this.snotifyService.success("Comment added", "Success!");
                        this.commentForm.reset();
                        this.comments = comments;
                        console.log(this.comments);

                    } 
                    else {
                        this.snotifyService.error("Comment wasn't add", "Error!");
                    }
                },
                err => {
                    this.snotifyService.error("Comment added", "Error!");
                });
    }

    public deleteComment(comment: Comment): void {
        
        this.complexStringService.deleteStringComment(comment.id, this.keyId)
            .subscribe(
                (comments) => {
                    if (comments) {
                        this.snotifyService.success("Comment delete", "Success!");
                        this.commentForm.reset();
                        this.comments = comments;
                        console.log(this.comments);
                    }
                    else {
                        this.snotifyService.error("Comment wasn't delete", "Error!");
                    }
                },
                err => {
                    this.snotifyService.error("Comment delete", "Error!");
                });
    }
}
