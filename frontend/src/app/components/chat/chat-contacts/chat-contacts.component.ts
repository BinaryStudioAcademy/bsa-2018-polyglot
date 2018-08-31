import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chat-contacts',
  templateUrl: './chat-contacts.component.html',
  styleUrls: ['./chat-contacts.component.sass']
})
export class ChatContactsComponent implements OnInit {
  step = 2;
  teamBadge = true;
  projectBadge = true;
  personBadge = false;

  private users: any;
  private projects: any;
  private teams: any;

  constructor() { }

  ngOnInit() {
        this.users = MOCK_USERS;
  }

  getProjects() {
    return;
    //debugger;
    //  if (this.projects) {
    //      return this.projects;
    //  }
    //  else {
    //    this.projectService.getAll().subscribe((projects) => {
    //      if(projects)
    //        {
    //          this.projects = projects;
    //        }
    //    });
    //  }
  }

  getTeams() {
    return;
    //debugger;
    //  if (this.teams) {
    //      return this.teams;
    //  }
    //  else {
    //    if(this.projects){
    //      for(let i = 0; i < this.projects.length; i++) {
    //        this.projectService.getProjectTeams(this.projects[i].id).subscribe((teams) => {
    //          if(teams)
    //            {
    //              Array.prototype.push.apply(this.teams, teams);
    //            }
    //        })
    //      }
    //    }
    //    
    //  }
  }

  setStep(index: number) {
      this.step = index;
  }

  nextStep() {
      this.step++;
  }

  prevStep() {
      this.step--;
  }
}


const MOCK_USERS = [
  {
      fullName: "Theodore Roosevelt",
      avatarUrl:
          "https://www.randomlists.com/img/people/theodore_roosevelt.jpg",
      isOnline: false
  },
  {
      fullName: "Jennifer Love Hewitt",
      avatarUrl:
          "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
      isOnline: true
  },
  {
      fullName: "Hugh Jackman",
      avatarUrl: "https://www.randomlists.com/img/people/hugh_jackman.jpg",
      isOnline: false
  },
  {
      fullName: "George Pal",
      avatarUrl: "https://www.randomlists.com/img/people/george_pal.jpg",
      isOnline: true
  },
  {
      fullName: "Tina Fey",
      avatarUrl: "https://www.randomlists.com/img/people/tina_fey.jpg",
      isOnline: true
  },
  {
      fullName: "Owen Wilson",
      avatarUrl: "https://www.randomlists.com/img/people/owen_wilson.jpg",
      isOnline: false
  },
  {
      fullName: "Robert De Niro",
      avatarUrl: "https://www.randomlists.com/img/people/robert_de_niro.jpg",
      isOnline: true
  },
  {
      fullName: "Julia Louis-Dreyfus",
      avatarUrl:
          "https://www.randomlists.com/img/people/julia_louis_dreyfus.jpg",
      isOnline: false
  },
  {
      fullName: "Natalya Rudakova",
      avatarUrl:
          "https://www.randomlists.com/img/people/natalya_rudakova.jpg",
      isOnline: true
  },
  {
      fullName: "Jennifer Love Hewitt",
      avatarUrl:
          "https://www.randomlists.com/img/people/jennifer_love_hewitt.jpg",
      isOnline: true
  },
  {
      fullName: "William Shatner",
      avatarUrl: "https://www.randomlists.com/img/people/william_shatner.jpg",
      isOnline: false
  }
];