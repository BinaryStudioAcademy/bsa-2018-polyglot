import { Component, OnInit, Input, SimpleChanges, ElementRef, ViewChild } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ComplexStringService } from '../../../../services/complex-string.service';
import { FormBuilder } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Comment } from '../../../../models/comment';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';
import { MatDialog } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { environment } from '../../../../../environments/environment';

@Component({
    selector: 'app-tab-comments',
    templateUrl: './tab-comments.component.html',
    styleUrls: ['./tab-comments.component.sass']
})
export class TabCommentsComponent implements OnInit {

    @Input() comments: Comment[];
    @ViewChild('textarea') textarea: ElementRef;

    newComment: any;
    public commentText: string;
    routeSub: Subscription;
    keyId: number;
    private url: string = environment.apiUrl;
    commentForm = this.fb.group({
        commentBody: ['',]
    });
    body: string;
    private currentPage = 0;
    private elementsOnPage = 7;

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
        this.currentPage = 0;
        this.complexStringService.createStringComment(this.newComment, this.keyId, this.elementsOnPage, this.currentPage)
            .subscribe(
                (comments) => {
                    if (comments) {
                        this.snotifyService.success("Comment added", "Success!");
                        this.commentForm.reset();
                        this.comments = comments;
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
                    }
                    else {
                        this.snotifyService.error("Comment wasn't delete", "Error!");
                    }
                },
                err => {
                    this.snotifyService.error("Comment delete", "Error!");
                });
    }

    public startEdittingComment(comment: Comment): void {
        this.commentText = comment.text;
        comment.isEditting = true;
    }

    public cancelEditting(comment: Comment): void {
        comment.isEditting = false;
    }

    public showCommentMenu(userId: number): boolean {
        if (this.userService.getCurrentUser().userRole === 1) {
            return true;
        }
        else if (this.userService.getCurrentUser().userRole === 0 && this.userService.getCurrentUser().id === userId) {
            return true;
        }
        else {
            return false;
        }
    }

    public editComment(comment: Comment, edittedText: string): void {
        comment.text = edittedText;
        this.complexStringService.editStringComment(comment, this.keyId)
            .subscribe(
                (comments) => {
                    if (comments) {
                        this.snotifyService.success("Comment edited", "Success!");
                        this.comments = comments;
                    }
                    else {
                        this.snotifyService.error("Comment wasn't edited", "Error!");
                    }
                },
                err => {
                    this.snotifyService.error("Comment edited", "Error!");
                });
    }

    public onScrollUp(): void {
        this.getComments(this.currentPage, comments => {
            this.comments = comments.concat(this.comments);
        });
    }

    public onScrollDown(): void {
        this.getComments(this.currentPage, comments => {
            this.comments = this.comments.concat(comments);
        });
    }

    getComments(page: number = 1, saveResultsCallback: (comments) => void) {
        return this.complexStringService
            .getCommentsWithPagination(
                this.keyId,
                this.elementsOnPage,
                this.currentPage + 1
            )
            .subscribe((comments: any) => {
                
                    this.currentPage++;
                    saveResultsCallback(comments);
                

            });
    }
}
