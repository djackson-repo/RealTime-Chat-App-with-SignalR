
    export class UserModel
    {
      public selected?= false;

      // these are database mapped, come from the API calls
      public id: string = '';
      public password: string = '';
      public name: string = '';
      public token: string = '';
      constructor(init?: Partial<UserModel>) { // allows us to assign this object C# style
        Object.assign(this, init); // copies all the properties of 'init' that exist in the 'this' object
      }
    }

