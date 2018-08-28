import { SnotifyDefaults } from "ng-snotify/snotify/interfaces/SnotifyDefaults.interface";

export const  SnotifyGlobalConfig: SnotifyDefaults = {
    global: {
      newOnTop: true,
      maxOnScreen: 8,
      maxAtPosition: 8,
    },
    toast: {
      type: 'simple',
      showProgressBar: false,
      timeout: 2000,
      closeOnClick: true,
      pauseOnHover: true,
      bodyMaxLength: 150,
      titleMaxLength: 16,
      backdrop: -1,
      icon: null,
      iconClass: null,
      html: null,
      position: 'rightBottom',
      animation: {enter: 'fadeIn', exit: 'fadeOut', time: 400}
    },
    type: {
      prompt: {
        timeout: 0,
        closeOnClick: false,
        buttons: [
          {text: 'Ok', action: null, bold: true},
          {text: 'Cancel', action: null, bold: false},
        ],
        placeholder: 'Enter answer here...',
        type: 'prompt',
      },
      confirm: {
        timeout: 0,
        closeOnClick: false,
        buttons: [
          {text: 'Ok', action: null, bold: true},
          {text: 'Cancel', action: null, bold: false},
        ],
        type: 'confirm',
      },
      simple: {
        type: 'simple',
        showProgressBar: false,
      },
      success: {
        type: 'success',
        showProgressBar: false,
      },
      error: {
        type: 'error',
        showProgressBar: false,
      },
      warning: {
        type: 'warning',
        showProgressBar: false,
      },
      info: {
        type: 'info',
        showProgressBar: false,
      },
      async: {
        pauseOnHover: false,
        closeOnClick: false,
        timeout: 0,
        showProgressBar: false,
        type: 'async'
      }
    }
  };