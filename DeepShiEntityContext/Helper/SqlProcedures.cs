using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepShiEntityContext.Helper
{
    public class SqlProcedures
    {
        public const string SPAddClients = "sp_insert_update_client";
        public const string SPPosts = "sp_insert_update_post";
        public const string SPCRCodes = "sp_cr_code";
        public const string SPCadres = "sp_cadre_code";

        public const string SPSpecialisitions = "sp_insert_update_specialization";
        public const string SPGetOrgPostTagWith = "sp_get_all_org_for_post_tag_with";
        public const string SPGetAllPostByOrg = "sp_get_all_posts_by_orgids";

        public const string SPAdditionalServicesMaster = "sp_insert_update_additionalservies_codes";
        public const string SPDispatchModeMaster = "sp_insert_update_dispatch_mode_codes";
        public const string SPExamTypesMaster = "sp_insert_update_exam_type_codes";
        public const string SPProjectHandledByMaster = "sp_insert_update_exam_handler_code";
        public const string SPRollNoStructureMaster = "sp_insert_update_rollnostructure_codes";
        public const string SPTimeTableMaster = "sp_insert_update_exam_time_table";
        public const string SPTypeOfTestMaster = "sp_insert_update_exam_test_type_code";

        public const string SPProjectInitiate = "sp_insert_update_project_doc";
        public const string SPProjectPostSpl = "sp_insert_update_project_org_post_cadre_specialization";
        public const string SPProjectSession = "sp_insert_update_project_org_time_table";
        public const string SPProjectCandidate = "sp_insert_update_project_org_candidate";
        public const string SPProjectOther = "sp_insert_update_project_other_details";
        public const string SPProjectDetailsList = "sp_project_List";
        public const string SPProjectChangedEffect = "sp_outside_transactions";

        public const string SPProjectSingles = "sp_section_wise_select";

        public const string SPProjectHistory = "sp_get_history_of_update_by_documnet_no";
        public const string SPProjectPostAndSpecialisation = "sp_get_post_specialization_by_project_documentno_for_test_structure";

        public const string SPMenuMaster = "sp_menumaster";
        public const string SPUserWiseMenuList = "sp_userwise_menu_mapping_list";
        public const string SPMenuPermissionAddRemove = "sp_add_remove_permission_menuitems";

        public const string SPProjeStatusChange = "sp_update_project_status";
        public const string SPPdfDataForProjectDetails = "sp_get_data_for_pdf";

        public const string SPEventCalander = "sp_calendar_table_insert_update";

        public const string SPMediumOfExam = "sp_insert_update_medium_of_exam_master";
        public const string SPTestSubjectName = "sp_insert_update_test_name_master";

        public const string SPExperienceMaster = "sp_insert_update_experience_remarks";
        public const string SPQualificationMaster = "sp_insert_update_qualification_remarks";

    }
}
