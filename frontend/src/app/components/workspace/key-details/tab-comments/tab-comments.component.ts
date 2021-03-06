import { Component, OnInit, Input, SimpleChanges, ElementRef, ViewChild, ViewEncapsulation } from '@angular/core';
import { UserService } from '../../../../services/user.service';
import { ComplexStringService } from '../../../../services/complex-string.service';
import { FormBuilder } from '@angular/forms';
import { SnotifyService } from 'ng-snotify';
import { Comment } from '../../../../models/comment';
import { ImgDialogComponent } from '../../../../dialogs/img-dialog/img-dialog.component';
import { MatDialog, MatList } from '@angular/material';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs';
import { environment } from '../../../../../environments/environment';
import { UserProfile } from '../../../../models';
import { MentionDirective, MentionModule } from 'angular2-mentions/mention';
import { ProjectService } from '../../../../services/project.service';

@Component({
    selector: 'app-tab-comments',
    templateUrl: './tab-comments.component.html',
    styleUrls: ['./tab-comments.component.sass']
    
})
export class TabCommentsComponent implements OnInit {

    @Input() comments: Comment[];
    @ViewChild(MatList, { read: ElementRef }) matList: ElementRef;

    public newComment: Comment;
    public commentText: string;
    public routeSub: Subscription;
    public keyId: number;
    @Input() textCommentForAdd: string;

    public commentForm = this.fb.group({
        commentBody: ['']
    });
    public body: string;
    public users : any[] = [];
    private url: string = environment.apiUrl;
    private currentPage = 0;
    private elementsOnPage = 7;

    constructor(private userService: UserService,
        private fb: FormBuilder,
        private complexStringService: ComplexStringService,
        private dialog: MatDialog,
        private snotifyService: SnotifyService,
        private activatedRoute: ActivatedRoute,
        private projectService: ProjectService) { }


    ngOnInit() {
        this.routeSub = this.activatedRoute.params.subscribe((params) => {
            this.keyId = params.keyId;
            this.complexStringService.getById(this.keyId).subscribe(data =>{
                this.projectService.getById(data.projectId).subscribe(proj =>{
                    this.users.push(proj.userProfile);
                    
                    this.projectService.getProjectTeams(proj.id).subscribe(teams =>{
                        teams.forEach(team => {
                            team.persons.forEach(element => {
                                this.users.push(element);
                            });
                        });
                        this.users = this.users.filter((user, index, self) =>   //remove duplicates
                            index === self.findIndex((t) => (
                                t.id === user.id
                            ))
                        )
                    })
                }) 
            });
        });
    }

    ngOnChanges(changes: SimpleChanges) {
        this.commentForm.reset();
        this.body = `-->${this.textCommentForAdd}<-- `;

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

                        setTimeout(() => {
                            this.matList.nativeElement.scrollTop = 0;
                        }, 100);
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

    public onScrollDown(): void {
        this.getComments(this.currentPage, comments => {
            this.comments = this.comments.concat(comments);
        });
    }

    public commentId(index, comment: Comment): string {
        return comment.id;
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
