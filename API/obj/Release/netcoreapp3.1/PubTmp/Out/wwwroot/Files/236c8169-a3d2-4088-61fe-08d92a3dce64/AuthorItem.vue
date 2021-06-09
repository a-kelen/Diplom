<template>
  <v-container>
    <v-sheet elevation="5" class="pa-3">
      <v-row class="mt-1">
        <v-col md="2">
            <v-avatar
            color="green"
            size="40"
            >
                <span class="white--text headline">AU</span>
            </v-avatar>
        </v-col>
        <v-col md="auto">
          <v-tooltip top nudge-top>
            <template v-slot:activator="{ on, attrs }">
               <div class="text-h5 text-truncate" v-bind="attrs"
                v-on="on">author-a</div>
            </template>
            <span>author-a</span>
          </v-tooltip>         
        </v-col>
      </v-row>
      <v-row>
        <v-col md="auto">
          <v-chip
            dark
            class="green"
          >12 elements</v-chip>
        </v-col>
        <v-col md="auto">
            <v-tooltip bottom>
                <template v-slot:activator="{ on, attrs }">
                    <v-icon v-bind="attrs"
                        v-on="on" class="my-1">
                        mdi-account-outline
                    </v-icon> 
                </template>
                <span>User</span>
            </v-tooltip>      
        </v-col>
      </v-row>
      <v-row>
        <v-col>
          <div class="grey--text pa-1 text--darken-1">Author Name</div>
        </v-col>
        <v-spacer/>
        <v-col md="auto">
           <div class="grey--text text-h6 text--darken-3">212 
               <v-icon 
                    v-bind="attrs"
                    v-on="on" class="mb-1">
                        mdi-thumb-up-outline
                </v-icon>
            </div>
        </v-col>
      </v-row>
    </v-sheet>
  </v-container>
</template>

<script>
export default {
  name: 'AuthorItem',
  data: () => ({
    rating: 4.5
  })
};
</script>
